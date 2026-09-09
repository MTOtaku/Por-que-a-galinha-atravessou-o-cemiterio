using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [Tooltip("Distância (em unidades) do centro da hitZone considerada Perfect. O restante, enquanto ainda dentro da HitZone, conta como Good.")]
    public float perfectDistance = 0.3f;
    
    [Header("Input System")]
    public InputActionReference attackUpAction;

    public InputActionReference attackDownAction;

    void OnEnable(){
        attackUpAction.action.Enable();
        attackDownAction.action.Enable();

        attackUpAction.action.performed += OnAttackUp;
        attackDownAction.action.performed += OnAttackDown;
    }

    void OnDisable(){
        attackUpAction.action.performed -= OnAttackUp;
        attackDownAction.action.performed -= OnAttackDown;

        attackUpAction.action.Disable();
        attackDownAction.action.Disable();
    }

    void OnAttackUp(InputAction.CallbackContext ctx) => TryHit(NoteType.Air);
    void OnAttackDown(InputAction.CallbackContext ctx) => TryHit(NoteType.Ground);
    
    void TryHit(NoteType type) {
        NoteHittable[] notes = FindObjectsOfType<NoteHittable>();
        foreach (var note in notes) {
            if (note.type == type && note.shape == NoteShape.Tap && note.InHitZone) {
                float distance = note.DistanceToHitZoneCenter();
                Judgement judgement = distance <= perfectDistance ? Judgement.Perfect : Judgement.Good;
                note.MarkAsHit();
                JudgementSystem.Instance.RegisterHit(judgement, note);
                return;
                //Fora disso já é miss
            }
        }
    }
}