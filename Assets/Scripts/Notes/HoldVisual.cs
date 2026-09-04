using UnityEngine;

[ExecuteAlways]
public class HoldVisual : MonoBehaviour {
    [Header("Duração do Hold")] [Tooltip("Quantos segundos o jogador precisa segurar")]
    public float holdDurationSeconds = 1f;

    [Header("Sincronia (tem que ta de acordo com o NoteTimingHandle)")]
    public float unitsPerBeat = 2f;

    public float bpm = 120f;

    [Header("Referências Visuais")] 
    public SpriteRenderer bodySpriteRenderer; //Draw mode precisa estar em Tiled
    public BoxCollider2D holdCollider;

    void OnValidate(){
        float secPerBeat = 60f / bpm;
        float speed = unitsPerBeat / secPerBeat;
        float bodyLength = holdDurationSeconds * speed;

        if (bodySpriteRenderer != null) {
            //Muda o comprimento
            bodySpriteRenderer.size = new Vector2(bodyLength, bodySpriteRenderer.size.y);
            
            //posiciona o corpo depois do head
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
