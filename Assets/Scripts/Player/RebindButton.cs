using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RebindButton : MonoBehaviour {
    public InputActionReference actionReference;
    public int bindingIndex = 0;
    public TMP_Text bindingDisplayText;

    private Button button;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private static readonly List<RebindButton> All = new();
    public static bool IsRebinding { get; private set; }

    void Awake() {
        button = GetComponent<Button>();
    }

    void OnEnable() {
        All.Add(this);
        UpdateDisplayText();
    }

    void OnDisable() {
        All.Remove(this);
    }

    public void StartRebind() {
        if (IsRebinding) return; 

        rebindingOperation?.Dispose();

        IsRebinding = true;
        SetOtherButtonsInteractable(false);

        InputAction action = actionReference.action;
        action.Disable();

        bindingDisplayText.text = "Pressione uma tecla...";

        rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(op => {
                action.Enable();
                UpdateDisplayText();
                SaveBindingOverride(action);
                FinishRebind();
            })
            .OnCancel(op => {
                action.Enable();
                UpdateDisplayText();
                FinishRebind();
            })
            .Start();
    }

    private void FinishRebind() {
        IsRebinding = false;
        SetOtherButtonsInteractable(true);
    }

    private void SetOtherButtonsInteractable(bool value) {
        foreach (var rb in All) {
            if (rb.button != null) rb.button.interactable = value;
        }
    }

    void UpdateDisplayText() {
        bindingDisplayText.text = actionReference.action.GetBindingDisplayString(bindingIndex);
    }

    void SaveBindingOverride(InputAction action) {
        string rebinds = action.actionMap.asset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("inputRebinds", rebinds);
    }

    void OnDestroy() {
        rebindingOperation?.Dispose();
        All.Remove(this);
    }
}