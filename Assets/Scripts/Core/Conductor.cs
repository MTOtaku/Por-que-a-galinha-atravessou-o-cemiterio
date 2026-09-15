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
    
    private bool isPaused = false;
    
    void Awake() {
        Instance = this;
    }

    public void StartSong() {
        dspSongTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update(){
        if (isPaused) return;
        SongPositionInSeconds = AudioSettings.dspTime - dspSongTime - firstBeatOffset;
        SongPositionInBeats = (float)(SongPositionInSeconds / rhythm.SecPerBeat);
    }

    public void Pause(){
        isPaused = true;
        musicSource.Pause();
    }

    public void Resume(){
        dspSongTime = AudioSettings.dspTime - firstBeatOffset - SongPositionInSeconds;
        
        isPaused = false;
        musicSource.UnPause();
    }
    
    void Start() {
        StartSong();
    }
}