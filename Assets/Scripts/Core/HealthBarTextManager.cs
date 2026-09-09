using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthBarTextManager : MonoBehaviour {
    public static HealthBarTextManager Instance;

    [Header("UI")] public TMP_Text healthText;

    void Awake(){
        Instance = this;
    }

    private void Update()
    {
        if (healthText != null && JudgementSystem.Instance != null)
        {
            healthText.text =
                $"HP: {JudgementSystem.Instance.health}";
        }
    }
    
}