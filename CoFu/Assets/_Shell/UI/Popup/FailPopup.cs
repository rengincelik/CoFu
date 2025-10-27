// FailPopup.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPopup : Popup
{
    // [SerializeField] SFXEventSO sFXEventSO;
    [SerializeField] AudioClip clip;
    
    [SerializeField] Button RestartButton;

    void OnEnable()
    {
        RestartButton.onClick.AddListener(OnButtonClicked);

    }
    void OnDisable()
    {
        RestartButton.onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        // sFXEventSO?.Raise(clip);

        AudioListenerManager.Instance.PlaySFX(clip);
        PopupManager.Instance.ClosePopup();
        ScreenManager.Instance.GoToLayerPlay();
    }

}

