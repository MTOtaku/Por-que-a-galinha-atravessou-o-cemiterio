using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    public enum ButtonSoundType { Accept,Cancel}

    public ButtonSoundType soundType = ButtonSoundType.Accept;

    public void OnPointerEnter(PointerEventData eventData){
        if (UISoundManager.Instance != null)
            UISoundManager.Instance.PlayHover();
    }

    public void OnPointerClick(PointerEventData eventData){
        if (UISoundManager.Instance == null) return;

        if (soundType == ButtonSoundType.Accept)
            UISoundManager.Instance.PlayAccept();
        else
            UISoundManager.Instance.PlayCancel();
    }
}