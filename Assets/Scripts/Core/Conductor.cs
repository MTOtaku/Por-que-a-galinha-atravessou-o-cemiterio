using UnityEngine;

public class Conductor : MonoBehaviour {
    public static Conductor Instance;

    [Header("Configuração da Música")]
    public AudioSource musicSource;
    public RhythmSettings rhythm;
    public float firstBeatOffset = 0f;

    public double dspSongTime;
    public double SongPositionInSeconds { get; private set; }
    public float SongPositionInBeats { get; private set; }

    void Awake() {
        Instance = this;
    }

    public void StartSong() {
        dspSongTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update() {
        SongPositionInSeconds = AudioSettings.dspTime - dspSongTime - firstBeatOffset;
        SongPositionInBeats = (float)(SongPositionInSeconds / rhythm.SecPerBeat);
    }

    void Start() {
        StartSong();
    }
}