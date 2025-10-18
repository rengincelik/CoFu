// PauseCommand.cs
using UnityEngine;

public class PauseCommand : ICommand
{
    public void Execute()
    {
        GameStateService.Instance.SetPaused(true);
        Time.timeScale = 0f; // Oyunu durdur
        Debug.Log("Game Paused");
    }

    public void Undo()
    {
        GameStateService.Instance.SetPaused(false);
        Time.timeScale = 1f;
    }
}
