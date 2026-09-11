using UnityEngine;
using UnityEngine.UI;

public class SongProgressUI : MonoBehaviour {
    public Image fillImage;

    void Update() {
        if (Conductor.Instance == null || fillImage == null) return;
        if (Conductor.Instance.musicSource == null || Conductor.Instance.musicSource.clip == null) return;

        float totalDuration = Conductor.Instance.musicSource.clip.length;
        if (totalDuration <= 0f) return;

        float progress = (float)Conductor.Instance.SongPositionInSeconds / totalDuration;
        fillImage.fillAmount = Mathf.Clamp01(progress);
    }
}