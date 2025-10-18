// FailPopup.cs
using TMPro;
using UnityEngine;

public class FailPopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI failText;
    [SerializeField] private GameObject addTimeButton;
    [SerializeField] private GameObject addMoveButton;
    
    protected override void OnShown()
    {
        // // Hangi button gösterilecek?
        // bool isTimedLevel = GameStateService.Instance.IsTimedLevel();
        
        // addTimeButton.SetActive(isTimedLevel);
        // addMoveButton.SetActive(!isTimedLevel);
    }
}

