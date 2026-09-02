using UnityEngine;

public class Conductor : MonoBehaviour {
    public static Conductor Instance;
    
    [Header("Configuração da Música")]
    public AudioSource musicSource;
    public float bpm = 120f;
    public float firstBeatOffset = 0f; // Pelo visto isto é segundos até o 1° beat se a musica tiver intro (pode ser alterado dependendo da musica)
    
    public float SecPerBeat { get; private set; } // Segundos por beat
    public double dspSongTime;
    
    public double SongPositionInSeconds { get; private set; }
    public float SongPositionInBeats { get; private set; }

    void Awake() {
        Instance = this;
        SecPerBeat = 60f / bpm;
    }

    public void StartSong() {
        dspSongTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update() {
        SongPositionInSeconds = AudioSettings.dspTime - dspSongTime - firstBeatOffset;
        SongPositionInBeats = (float)(SongPositionInSeconds / SecPerBeat);
    }

}
