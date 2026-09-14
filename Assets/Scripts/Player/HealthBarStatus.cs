using UnityEngine;
using UnityEngine.UI;

public class HealthBarStatus : MonoBehaviour {
    public Image statusImage;
    
    public Sprite StatusBar1;
    public Sprite StatusBar2;
    public Sprite StatusBar3;
    
    void Update(){
        if (JudgementSystem.Instance == null || statusImage == null) return;

        float health = JudgementSystem.Instance.health;

        if (health > 70f) {
            statusImage.sprite = StatusBar1;
        }
        else if (health > 30f) {
            statusImage.sprite = StatusBar2;
        }
        else {
            statusImage.sprite = StatusBar3;
        }
    }
}
