using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public enum NoteShape { Tap, Hold }

public class NoteHittable : MonoBehaviour {
    public NoteType type;
    public NoteShape shape = NoteShape.Tap;
    public bool InHitZone { get; private set; }

    [Header("Nota")] [Tooltip("Tempo pra nota ser destruida no miss")]
    public float missDestroyDelay = 1.0f;

    [Header("Input System")]
    public InputActionReference attackUpAction;
    public InputActionReference attackDownAction;

    private Transform hitZoneTransform;
    private bool wasHit = false; // Isso aq é pra nota de Tap
    private bool heldAtSomePoint = false; // ISso aq é pra notas de Hold
    
    public static readonly System.Collections.Generic.List<NoteHittable> Active = new();

    void OnEnable() => Active.Add(this);
    void OnDisable() => Active.Remove(this);
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HitZone")) {
            InHitZone = true;
            hitZoneTransform = other.transform;
        }
    }

    void Update(){
        if (shape == NoteShape.Hold && InHitZone) {
            InputAction action = type == NoteType.Air ? attackUpAction.action : attackDownAction.action;
            if (action.IsPressed()) heldAtSomePoint = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (!other.CompareTag("HitZone")) return;
        InHitZone = false;

        if (shape == NoteShape.Tap) {
            if (!wasHit) {
                JudgementSystem.Instance.RegisterMiss();
                print("Miss - NoteHittable.cs");

                StartCoroutine(DestroyAfterMiss());
                return;
            }
        } else {
            InputAction action = type == NoteType.Air ? attackUpAction.action : attackDownAction.action;
            bool stillHolding = action.IsPressed();

            if (stillHolding) JudgementSystem.Instance.RegisterHit(Judgement.Perfect, this);
            else if (heldAtSomePoint) JudgementSystem.Instance.RegisterHit(Judgement.Good, this);
            else {
                JudgementSystem.Instance.RegisterMiss();
                StartCoroutine(DestroyAfterMiss());
                return;
            }
        }
        Destroy(gameObject);
    }

    public float DistanceToHitZoneCenter() {
        if (hitZoneTransform == null) return float.MaxValue;
        return Mathf.Abs(transform.position.x - hitZoneTransform.position.x);
    }

    public void MarkAsHit() {
        wasHit = true;
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterMiss(){
        yield return new WaitForSeconds(missDestroyDelay);
        Destroy(gameObject);
    }
}