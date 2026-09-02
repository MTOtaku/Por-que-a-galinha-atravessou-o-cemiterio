using UnityEngine;

public class NoteSpawner : MonoBehaviour {
    public ChartData chart;
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform hitZone;
    public float noteTravelTime = 2f; //Tempo da nota ir pro spawn até a area de hit

    private int nextNoteIndex = 0;

    void Update() {
        if (nextNoteIndex >= chart.notes.Count) return;

        float secPerBeat = 60f / chart.bpm;
        float noteTimeInSeconds = chart.notes[nextNoteIndex].beat * secPerBeat;
        
        if (Conductor.Instance.SongPositionInSeconds >= noteTimeInSeconds - noteTravelTime) {
            SpawnNote(chart.notes[nextNoteIndex], noteTimeInSeconds);
            nextNoteIndex++;
        }
    }

    void SpawnNote(NoteEvent noteEvent, float targetTime)  {
        GameObject obj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        Note note = obj.GetComponent<Note>();
        note.Init(noteEvent.type, targetTime, spawnPoint.position, hitZone.position, noteTravelTime);
    }
}