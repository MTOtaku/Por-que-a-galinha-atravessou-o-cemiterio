using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    public Image fillImage;

    private void Update(){
        if (JudgementSystem.Instance == null || fillImage == null) return;
        
        fillImage.fillAmount = Mathf.Clamp01(JudgementSystem.Instance.health / JudgementSystem.Instance.maxHealth);
    }
}