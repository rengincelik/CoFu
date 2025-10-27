
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : Popup
{
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
        PopupManager.Instance.ClosePopup();
        ScreenManager.Instance.GoToLayerPlay();
    }


}


