
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : Popup
{
    // [SerializeField] SFXEventSO sFXEventSO;
    [SerializeField] AudioClip clip;
    [SerializeField] Button ContinueButton;

    void OnEnable()
    {
        ContinueButton.onClick.AddListener(OnButtonClicked);

    }
    void OnDisable()
    {
        ContinueButton.onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        // sFXEventSO?.Raise(clip);
        AudioListenerManager.Instance.PlaySFX(clip);
        PopupManager.Instance.ClosePopup();
        ScreenManager.Instance.GoToLayerPlay();
    }


}


