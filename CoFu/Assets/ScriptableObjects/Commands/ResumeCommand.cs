// ResumeCommand.cs
using UnityEngine;

public class ResumeCommand : ICommand
{
    public void Execute()
    {
        GameStateService.Instance.SetPaused(false);
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    public void Undo()
    {
        GameStateService.Instance.SetPaused(true);
        Time.timeScale = 0f;
    }
}
