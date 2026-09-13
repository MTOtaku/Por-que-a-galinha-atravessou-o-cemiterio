using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsUI : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start() {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float value) {
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(value));
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value) {
        audioMixer.SetFloat("SFXVolume", LinearToDecibel(value));
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private float LinearToDecibel(float value) {
        return value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
    }
}