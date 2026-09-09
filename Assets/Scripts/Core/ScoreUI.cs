using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour {
    public TMP_Text scoreText;

    void Update(){
        if (JudgementSystem.Instance == null || scoreText == null) return;
        
        scoreText.text = $"Score: {JudgementSystem.Instance.score}";
    }
}