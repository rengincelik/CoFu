using UnityEngine;

public class CommandGenerator : MonoBehaviour
{
    public CommandEventSO commandEventSO;
    public GameStateChangedEventSO stateEvent;
    public AudioEventSO audioEvent;
    public ScreenEventSO screenEvent;
    public JokerEventSO jokerEvent;

    [ContextMenu("Level Start Command")]
    void LevelStart()
    {
        var cmd = new LevelStartCommand(
            GameStateService.Instance,
            stateEvent,
            audioEvent
        );
        commandEventSO.Raise(cmd);
    }

    [ContextMenu("Go To Screen Command")]
    void GoToScreen()
    {
        var cmd = new GoToScreenCommand(ScreenViewType.Play, screenEvent);
        commandEventSO.Raise(cmd);
    }

    [ContextMenu("Use Joker Command")]
    void UseJoker()
    {
        var cmd = new UseJokerCommand(JokerType.AddTile, jokerEvent);
        commandEventSO.Raise(cmd);
    }
}
