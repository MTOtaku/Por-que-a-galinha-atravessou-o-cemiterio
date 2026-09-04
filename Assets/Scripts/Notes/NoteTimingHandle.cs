using UnityEngine;

[ExecuteAlways] // roda até fora do Play Mode
public class NoteTimingHandle : MonoBehaviour {
    [Tooltip("Tempo em segundos da música em que a nota deve ser acertada. Pegue esse valor olhando a onda sonora no Audacity (ou similar) — bem mais fácil que calcular beat.")]
    public float timeInSeconds;

    public RhythmSettings rhythm;
    void OnValidate(){
        if (rhythm == null) return;

        Vector3 pos = transform.localPosition;
        pos.x = timeInSeconds * rhythm.Speed; // posição relativa ao NoteTrack (pai)
        transform.localPosition = pos;
    }
}