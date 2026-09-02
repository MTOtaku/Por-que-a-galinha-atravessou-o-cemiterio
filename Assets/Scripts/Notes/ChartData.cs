using UnityEngine;
using System.Collections.Generic;

// acho que isso seria o chart?

public enum NoteType { AttackUp, AttackDown } //  Quantidade de notas, no caso 2?

[System.Serializable]
public class NoteEvent {
    public float beat;      // em que beat a nota acontece
    public NoteType type;
}

[CreateAssetMenu(fileName = "NewChart", menuName = "RitmoGalinha/Chart")]
public class ChartData : ScriptableObject {
    public AudioClip song;
    public float bpm;
    public List<NoteEvent> notes = new List<NoteEvent>();
}