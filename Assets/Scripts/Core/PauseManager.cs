using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour {
    public static PauseManager Instance;

    [Header("UI")]
    public GameObject pausePanel;

    [Header("Input System")]
    public InputActionReference pauseAction;

    private bool isPaused = false;

    void Awake() {
        Instance = this;
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    void OnEnable() {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPausePressed;
    }

    void OnDisable() {
        pauseAction.action.performed -= OnPausePressed;
        pauseAction.action.Disable();
    }

    void OnPausePressed(InputAction.CallbackContext ctx) {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause() {
        isPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;

        if (Conductor.Instance != null && Conductor.Instance.musicSource != null)
            Conductor.Instance.musicSource.Pause();
    }

    public void Resume() {
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;

        if (Conductor.Instance != null && Conductor.Instance.musicSource != null)
            Conductor.Instance.musicSource.UnPause(); 
    }
}