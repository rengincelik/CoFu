// ValueDisplay.cs
using UnityEngine;
using TMPro;

public class ValueDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private DisplayType displayType;
    
    [Header("Events")]
    [SerializeField] private GameEventSO updateEvent; // hangi event'i dinleyecek

    void OnEnable()
    {
        updateEvent?.AddListener(UpdateDisplay);
        UpdateDisplay(); // İlk değer
    }

    void OnDisable()
    {
        updateEvent?.RemoveListener(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        int value = displayType switch
        {
            DisplayType.Score => GameStateService.Instance.GetScore(),
            DisplayType.Coins => GameStateService.Instance.GetCoins(),
            DisplayType.Jokers => GameStateService.Instance.GetJokersRemaining(),
            _ => 0
        };
        
        text.text = value.ToString();
    }
}

public enum DisplayType
{
    Score,
    Coins,
    Jokers
}
