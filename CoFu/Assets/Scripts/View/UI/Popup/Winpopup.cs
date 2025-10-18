// WinPopup.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsEarnedText;
    [SerializeField] private Button ContinueButton;
    
    protected override void OnShown()
    {
        // int level = GameStateService.Instance.GetLevel();
        // int coins = GameStateService.Instance.GetCoins();
        
        // levelText.text = $"Level {level} Complete!";
        // coinsEarnedText.text = $"+{coins} Coins";
    }
}


