using UnityEngine;

public class CreditsPanelToggle : MonoBehaviour {
    public GameObject creditsPanel;

    void Start() {
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void OpenCredits() {
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void CloseCredits() {
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }
}