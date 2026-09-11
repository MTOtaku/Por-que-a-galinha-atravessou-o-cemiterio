using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager Instance;
    public NorgetController norget;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TMP_Text scoreText;

    [Header("HUD")]
    public GameObject hud;

    [Header("Cenas")] public string menuSceneName = "Menu";

    void Awake(){
        Instance = this;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(){
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (hud != null) hud.SetActive(false);

        if(scoreText != null && JudgementSystem.Instance != null)
            scoreText.text = $"Score: {JudgementSystem.Instance.score}";

        Time.timeScale = 0f;

        if (Conductor.Instance != null && Conductor.Instance.musicSource != null) Conductor.Instance.musicSource.Pause();
    }

    public void Restart() {
        SceneManager.sceneLoaded += OnSceneReload;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu() {
        SceneManager.sceneLoaded += OnSceneReload;
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnSceneReload(Scene scene, LoadSceneMode mode){
        Time.timeScale = 1f;
        SceneManager.sceneLoaded -= OnSceneReload;
    }

    public void StartDeathSequence(){
        Time.timeScale = 0f;

        if (Conductor.Instance != null && Conductor.Instance.musicSource != null)
            Conductor.Instance.musicSource.Pause();

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation(){
        yield return new WaitForSecondsRealtime(1.0f);

        ShowGameOver();
    }
}