using UnityEngine;

public class ImpactPiece : MonoBehaviour {
    [Tooltip("Direção do arremesso, ajuste como preferir no Editor")]
    public Vector2 launchDirection = new Vector2(1f, 1f);
    public float launchForce = 5f;
    public float lifetime = 2f;
    
    [Header("Giro (alternativa sem Animator)")]
    public float spinSpeed = 360f; // graus por segundo

    void Update(){
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
    
    public void Init(Sprite sprite, Vector3 scale) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = sprite;

        transform.localScale = scale;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);

        Destroy(gameObject, lifetime);
    }
}