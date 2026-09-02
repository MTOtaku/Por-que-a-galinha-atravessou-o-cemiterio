using UnityEngine;
public class NoteHittable : MonoBehaviour {
    public NoteType type;
    public bool InHitZone { get; private set; }

    private Transform hitZoneTransform;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HitZone")) {
            InHitZone = true;
            hitZoneTransform = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("HitZone")) {
            InHitZone = false;
            if (!wasHit){
                JudgementSystem.Instance.RegisterMiss();
                print("Note Miss - NoteHittable.cs");
            }
            Destroy(gameObject);
        }
    }
    
    // Distancia até o centro da hitzone dentro da unidade da unity
    public float DistanceToHitZoneCenter() {
        if (hitZoneTransform == null) return float.MaxValue;
        return Mathf.Abs(transform.position.x - hitZoneTransform.position.x);
    }
    
    private bool wasHit = false;

    public void MarkAsHit() {
        wasHit = true;
        print("Note Hit - NoteHittable.cs");
        Destroy(gameObject);
    }
}

