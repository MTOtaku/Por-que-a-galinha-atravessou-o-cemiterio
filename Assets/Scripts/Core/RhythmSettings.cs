using UnityEngine;

[CreateAssetMenu(fileName = "NewRhythmSettings", menuName = "Ritmo Musica")]
public class RhythmSettings : ScriptableObject {
    public float bpm = 120f;
    public float unitsPerBeat = 2f;

    public float SecPerBeat => 60f / bpm;
    public float Speed => unitsPerBeat / SecPerBeat; // Unidades por Segundo
}