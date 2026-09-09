using UnityEngine;

public class NoteTrackScroller : MonoBehaviour {
    public RhythmSettings rhythm;
    private void Update() {
        transform.position += Vector3.left * (rhythm.Speed * Time.deltaTime);
    }
}
