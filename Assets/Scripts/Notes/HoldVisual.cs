using UnityEngine;

[ExecuteAlways]
public class HoldVisual : MonoBehaviour {
    [Header("Duração do Hold")]
    public float holdDurationSeconds = 1f;

    public RhythmSettings rhythm;

    [Header("Referências Visuais")]
    public SpriteRenderer bodySpriteRenderer;
    public BoxCollider2D holdCollider;

    void OnValidate() {
        if (rhythm == null) return;

        float bodyLength = holdDurationSeconds * rhythm.Speed;

        if (bodySpriteRenderer != null) {
            bodySpriteRenderer.size = new Vector2(bodyLength, bodySpriteRenderer.size.y);
            Vector3 pos = bodySpriteRenderer.transform.localPosition;
            pos.x = bodyLength / 2f;
            bodySpriteRenderer.transform.localPosition = pos;
        }

        if (holdCollider != null) {
            holdCollider.size = new Vector2(bodyLength, holdCollider.size.y);
            holdCollider.offset = new Vector2(bodyLength / 2f, holdCollider.offset.y);
        }
    }
}