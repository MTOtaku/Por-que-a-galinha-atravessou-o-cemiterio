using UnityEngine;
using UnityEngine.InputSystem;

public enum NoteShape { Tap, Hold }

public class NoteHittable : MonoBehaviour {
    public NoteType type;
    public NoteShape shape = NoteShape.Tap;
    public bool InHitZone { get; private set; }

    [Header("Input System")]
    public InputActionReference attackUpAction;

    public InputActionReference attackDownAction;
    
    private Transform hitZoneTransform;
    private bool wasHit = false; // Isso aq é pra nota de Tap
    private bool heldAtSomePoint = false; // ISso aq é pra notas de Hold
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HitZone")) {
            InHitZone = true;
            hitZoneTransform = other.transform;
        }
    }

    void Update(){
        if (shape == NoteShape.Hold && InHitZone) {
            KeyCode key = type == NoteType.Air ? KeyCode.A : KeyCode.S;
            if (Input.GetKey(key)) heldAtSomePoint = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if (!other.CompareTag("HitZone")) return;
        InHitZone = false;

        if (shape == NoteShape.Tap) {
            if (!wasHit) {
                JudgementSystem.Instance.RegisterMiss();
                print("Miss - NoteHittable.cs");
            }
        } else { //Hold notes
            InputAction action = type == NoteType.Air ? attackUpAction.action : attackDownAction.action;
            bool stillHolding = action.IsPressed();
            
            if (stillHolding) JudgementSystem.Instance.RegisterHit(Judgement.Perfect,this);
            else if (heldAtSomePoint) JudgementSystem.Instance.RegisterHit(Judgement.Good, this);
            else JudgementSystem.Instance.RegisterMiss();
        }
        Destroy(gameObject);
    }
    
    // Distancia até o centro da hitzone dentro da unidade da unity
    public float DistanceToHitZoneCenter() {
        if (hitZoneTransform == null) return float.MaxValue;
        return Mathf.Abs(transform.position.x - hitZoneTransform.position.x);
    }
    
    public void MarkAsHit() {
        wasHit = true;
        Destroy(gameObject);
    }
}

