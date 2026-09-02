using UnityEngine;

[ExecuteAlways] // roda até fora do Play Mode
public class NoteTimingHandle : MonoBehaviour {
    [Tooltip("Tempo em segundos da música em que a nota deve ser acertada. Pegue esse valor olhando a onda sonora no Audacity (ou similar) — bem mais fácil que calcular beat.")]
    public float timeInSeconds;

    [Tooltip("Precisa bater com o unitsPerBeat do NoteTrackScroller")]
    public float unitsPerBeat = 2f;

    [Tooltip("Precisa bater com o bpm do Conductor")]
    public float bpm = 120f;

    void OnValidate() {
        float secPerBeat = 60f / bpm;
        float speed = unitsPerBeat / secPerBeat;

        Vector3 pos = transform.localPosition;
        pos.x = timeInSeconds * speed; // posição relativa ao NoteTrack (pai)
        transform.localPosition = pos;
    }
}