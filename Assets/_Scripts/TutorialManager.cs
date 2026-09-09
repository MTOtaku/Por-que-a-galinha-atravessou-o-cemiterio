using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialStep
    {
        Intro,
        DownLaneAndGroundAttack,
        UpLaneAndJumpAttack,
        ObstaclesAndHitzone,
        HealthAndSneakers,
        Countdown,
        Completed
    }

    [Header("Componentes de Interface (UI)")]
    [SerializeField] private GameObject tutorialDialogPanel; // O painel de fundo do tutorial
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private Button nextStepButton;
    [SerializeField] private TMP_Text buttonLabelText;
    [SerializeField] private TMP_Text countdownText;

    [Header("Ambiente de Jogo")]
    [Tooltip("Arraste o objeto Environment para cá para garantir que o paralaxe rode no tutorial.")]
    [SerializeField] private GameObject environmentObject;

    [Header("Cena de Destino")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    private TutorialStep currentStep = TutorialStep.Intro;
    private bool isCountingDown = false;

    private void Start()
    {
        // Garante que o cenário paralaxe comece rodando visível no tutorial
        if (environmentObject != null)
        {
            environmentObject.SetActive(true);
        }

        if (nextStepButton != null)
        {
            nextStepButton.onClick.RemoveAllListeners();
            nextStepButton.onClick.AddListener(AdvanceStep);
        }
        
        if (countdownText != null) 
        {
            countdownText.gameObject.SetActive(false);
        }

        UpdateStepUI();
    }

    private void Update()
    {
        if (isCountingDown || Keyboard.current == null) return;

        switch (currentStep)
        {
            case TutorialStep.DownLaneAndGroundAttack:
                if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.kKey.wasPressedThisFrame)
                {
                    AdvanceStep();
                }
                break;

            case TutorialStep.UpLaneAndJumpAttack:
                if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.fKey.wasPressedThisFrame)
                {
                    AdvanceStep();
                }
                break;
        }
    }

    public void AdvanceStep()
    {
        if (isCountingDown || currentStep == TutorialStep.Completed) return;

        currentStep++;

        if (currentStep == TutorialStep.Countdown)
        {
            StartCoroutine(TutorialCountdownRoutine());
        }
        else
        {
            UpdateStepUI();
        }
    }

    private void UpdateStepUI()
    {
        // Garante que o painel e os textos de instrução estejam ativos nas etapas normais
        if (tutorialDialogPanel != null) tutorialDialogPanel.SetActive(true);
        if (countdownText != null) countdownText.gameObject.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(true);
        if (nextStepButton != null) nextStepButton.gameObject.SetActive(true);

        switch (currentStep)
        {
            case TutorialStep.Intro:
                instructionText.text = "<b>BEM-VINDO AO CEMITÉRIO!</b>\nNorget é uma galinha de tênis perdida na escuridão. Corra no ritmo para sobreviver às criaturas!\n\n<i>Clique em 'Próximo' para continuar.</i>";
                if (buttonLabelText != null) buttonLabelText.text = "Próximo";
                break;

            case TutorialStep.DownLaneAndGroundAttack:
                instructionText.text = "<b>1. LINHA INFERIOR (Down Lane - DL)</b>\nPor padrão, Norget corre na linha de baixo.\nPressione <b>J</b> ou <b>K</b> para realizar o <b>Ground Attack</b> e derrotar inimigos terrestres (como a Raposa Zumbi)!";
                if (buttonLabelText != null) buttonLabelText.text = "Pressione J ou K";
                break;

            case TutorialStep.UpLaneAndJumpAttack:
                instructionText.text = "<b>2. LINHA SUPERIOR (Up Lane - UL)</b>\nPressione <b>D</b> ou <b>F</b> para realizar o <b>Jump & Jump Attack</b>!\nIsso faz Norget pular para a linha de cima, atacando inimigos aéreos (Gárgulas) ou desviando de obstáculos!";
                if (buttonLabelText != null) buttonLabelText.text = "Pressione D ou F";
                break;

            case TutorialStep.ObstaclesAndHitzone:
                instructionText.text = "<b>3. HITZONES E RITMO</b>\n• Pressione as teclas exatamente dentro do círculo da Hitzone para pontuar (<b>Perfect</b> ou <b>Good</b>).\n• Obstáculos (Pedestais e Lápides) <b>não podem ser atacados</b>: troque de linha para desviar!";
                if (buttonLabelText != null) buttonLabelText.text = "Entendi";
                break;

            case TutorialStep.HealthAndSneakers:
                instructionText.text = "<b>4. VIDA E TÊNIS DE NORGET</b>\nNorget começa com seus 2 tênis. A cada dano sofrido, ela perde um tênis.\nSe ficar descalça (Estágio 1), o próximo dano será fatal!\n\n<i>Clique em 'Começar' para iniciar!</i>";
                if (buttonLabelText != null) buttonLabelText.text = "Começar!";
                break;
        }
    }

    private IEnumerator TutorialCountdownRoutine()
    {
        isCountingDown = true;

        // Desativa o painel de fundo e os textos de instrução durante a contagem
        if (tutorialDialogPanel != null) tutorialDialogPanel.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(false);
        if (nextStepButton != null) nextStepButton.gameObject.SetActive(false);

        // Ativa o texto da contagem centralizado na tela
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);

            countdownText.text = "3";
            yield return new WaitForSeconds(1f);
            
            countdownText.text = "2";
            yield return new WaitForSeconds(1f);
            
            countdownText.text = "1";
            yield return new WaitForSeconds(1f);
            
            countdownText.text = "CÓ!";
            yield return new WaitForSeconds(0.5f);
        }

        currentStep = TutorialStep.Completed;
        SceneManager.LoadScene(gameplaySceneName);
    }
}