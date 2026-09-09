using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryManager : MonoBehaviour {
    public static VictoryManager Instance;

    [Header("UI")] public GameObject victoryPanel;
    public TMP_Text scoreText;
    public TMP_Text perfectText;
    public TMP_Text goodText;
    public TMP_Text missText;

    [Header("Cenas")] public string menuSceneName = "Menu";

    void Awake(){
        Instance = this;
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    public void ShowVictory(){
        if (victoryPanel != null) victoryPanel.SetActive(true);

        if (JudgementSystem.Instance != null) {
            var js = JudgementSystem.Instance;
            if (scoreText != null) scoreText.text = $"Score: {js.score}";
            if (perfectText != null) perfectText.text = $"Perfect: {js.PerfectCount}";
            if (goodText != null) goodText.text = $"Good: {js.GoodCount}";
            if (missText != null) missText.text = $"Miss: {js.MissCount}";
        }

        Time.timeScale = 0f;
        
        if (Conductor.Instance != null && Conductor.Instance.musicSource != null) Conductor.Instance.musicSource.Pause();
    }

    public void Restart(){
        SceneManager.sceneLoaded += OnSceneReloaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu(){
        SceneManager.sceneLoaded += OnSceneReloaded;
        SceneManager.LoadScene(menuSceneName);
    }

    public void OnSceneReloaded(Scene scene, LoadSceneMode mode){
        Time.timeScale = 1f;
        SceneManager.sceneLoaded -= OnSceneReloaded;
    }
}