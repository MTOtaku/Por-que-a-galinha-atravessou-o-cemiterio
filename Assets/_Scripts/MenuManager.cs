using UnityEngine;
using UnityEngine.InputSystem; // Namespace obrigatório para o New Input System

// MenuManager.cs
// Gerencia os estados de fluxo do jogo: Menu Principal, Gameplay Ativo, Pause via Espaço e HUD.
public class MenuManager : MonoBehaviour
{
    [Header("Painéis de Interface (UI)")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject inGameHudPanel;

    [Header("Ambiente de Jogo")]
    [Tooltip("Referência para o objeto pai do cenário em paralaxe.")]
    [SerializeField] private GameObject environmentObject;

    private bool isPlaying = false;
    private bool isPaused = false;

    private void Start()
    {
        OpenMainMenu();
    }

    private void Update()
    {
        // Utiliza o New Input System (Keyboard.current) para evitar conflitos com a classe legada UnityEngine.Input
        if (isPlaying && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);
        SetEnvironmentActive(false);
        isPlaying = false;
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f; 
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);
        SetEnvironmentActive(false);
        isPlaying = false;
        isPaused = false;
    }

    public void StartGamePlay()
    {
        Time.timeScale = 1f;
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);

        SetEnvironmentActive(true);
        isPlaying = true;
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; 
            if (inGameHudPanel != null) inGameHudPanel.SetActive(true);
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);
    }

    public void ReturnToMenuFromGame()
    {
        ResumeGame(); 
        OpenMainMenu();
    }

    private void SetEnvironmentActive(bool isActive)
    {
        if (environmentObject != null)
        {
            environmentObject.SetActive(isActive);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}