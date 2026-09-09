using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// MenuManager.cs
// Gerencia todas as telas do menu principal, transições de cena e estado de pausa.
public class MenuManager : MonoBehaviour
{
    [Header("Painéis de Interface (UI)")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject inGameHudPanel;

    [Header("Ambiente e Elementos de Jogo")]
    [Tooltip("Referência para o objeto pai do cenário em paralaxe.")]
    [SerializeField] private GameObject environmentObject;
    [Tooltip("Objeto pai que agrupa o Player, Hitzones e sistemas da partida.")]
    [SerializeField] private GameObject gameplayElementsContainer;

    [Header("Configuração de Cenas")]
    [SerializeField] private string tutorialSceneName = "Tutorial";
    [SerializeField] private string gameplaySceneName = "Gameplay";

    private bool isPlaying = false;
    private bool isPaused = false;

    private void Start()
    {
        OpenMainMenu();
    }

    private void Update()
    {
        if (isPlaying && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);

        SetEnvironmentActive(true);
        SetGameplayElementsActive(false);
        isPlaying = false;
        isPaused = false;
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);

        SetEnvironmentActive(false);
        SetGameplayElementsActive(false);
        isPlaying = false;
    }

    // Função para iniciar o Tutorial (conforme o GDD: botão Jogar abre o tutorial)
    public void StartTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(tutorialSceneName);
    }

    // Ativa os elementos de gameplay e oculta o menu na mesma cena
    public void StartGameDirectly()
    {
        Time.timeScale = 1f;
        
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) mainMenuPanel.SetActive(false);
        if (inGameHudPanel != null) mainMenuPanel.SetActive(false);

        SetEnvironmentActive(true);
        SetGameplayElementsActive(true);

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

    private void SetGameplayElementsActive(bool isActive)
    {
        if (gameplayElementsContainer != null)
        {
            gameplayElementsContainer.SetActive(isActive);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}