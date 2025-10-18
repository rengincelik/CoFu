// PausePopup.cs
using UnityEngine;

public class WinPopup : BasePopup
{
    [Header("Events")]
    [SerializeField] private GameEventSO onGameStateChanged;

    void OnEnable()
    {
        onGameStateChanged?.AddListener(CheckPauseState);
    }

    void OnDisable()
    {
        onGameStateChanged?.RemoveListener(CheckPauseState);
    }

    private void CheckPauseState()
    {
        if (GameStateService.Instance.IsPaused())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    protected override void OnShown()
    {
        // Pause popup açıldığında yapılacaklar
        Debug.Log("Pause Popup Shown");
    }
}
