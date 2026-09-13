using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebindLoader : MonoBehaviour {
    public InputActionAsset actions;

    void Awake() {
        string rebinds = PlayerPrefs.GetString("inputRebinds", string.Empty);
        if (!string.IsNullOrEmpty(rebinds)) {
            actions.LoadBindingOverridesFromJson(rebinds);
        }
    }
}