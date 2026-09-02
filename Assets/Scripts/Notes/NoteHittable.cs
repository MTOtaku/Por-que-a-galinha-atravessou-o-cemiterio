using UnityEngine;
public class NoteHittable : MonoBehaviour {
    public NoteType type;
    public bool InHitZone { get; private set; }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HitZone")) InHitZone = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitZone")) {
            InHitZone = false;
            if (!wasHit){
                JudgementSystem.Instance.RegisterMiss();
                print("Note Miss - NoteHittable.cs");
            }
            Destroy(gameObject);
        }
    }
    
    private bool wasHit = false;

    public void MarkAsHit() {
        wasHit = true;
        print("Note Hit - NoteHittable.cs");
        Destroy(gameObject);
    }
}

