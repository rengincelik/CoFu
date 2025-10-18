// GameStateListener.cs - MonoBehaviour (Bootstrap scene'de)
using UnityEngine;

public class GameStateListener : MonoBehaviour
{
    [Header("State Events")]
    [SerializeField] private GameEventSO onPausedEvent;
    [SerializeField] private GameEventSO onResumedEvent;
    [SerializeField] private GameEventSO onLevelCompleteEvent;
    [SerializeField] private GameEventSO onLevelFailedEvent;

    [Header("Command Event")]
    [SerializeField] private CommandEventSO commandEvent;

    void OnEnable()
    {
        onPausedEvent?.AddListener(HandlePaused);
        onResumedEvent?.AddListener(HandleResumed);
        onLevelCompleteEvent?.AddListener(HandleLevelComplete);
        onLevelFailedEvent?.AddListener(HandleLevelFailed);
    }

    void OnDisable()
    {
        onPausedEvent?.RemoveListener(HandlePaused);
        onResumedEvent?.RemoveListener(HandleResumed);
        onLevelCompleteEvent?.RemoveListener(HandleLevelComplete);
        onLevelFailedEvent?.RemoveListener(HandleLevelFailed);
    }

    void HandlePaused()
    {
        // // Command'ları sırayla tetikle
        // commandEvent.Raise(new SetTimeScaleCommand(0f));
        // commandEvent.Raise(new PlayAudioCommand(AudioType.Pause));
        // commandEvent.Raise(new ShowPopupCommand(PopupType.Pause));
    }

    void HandleResumed()
    {
        // commandEvent.Raise(new SetTimeScaleCommand(1f));
        // commandEvent.Raise(new PlayAudioCommand(AudioType.Resume));
    }

    void HandleLevelComplete()
    {
        // commandEvent.Raise(new SetTimeScaleCommand(0f));
        // commandEvent.Raise(new PlayAudioCommand(AudioType.LevelWin));
        // commandEvent.Raise(new ModifyResourceCommand(ResourceType.Live, 1)); // Can kazan
        // commandEvent.Raise(new ShowPopupCommand(PopupType.Win));
        // commandEvent.Raise(new SaveGameCommand());
    }

    void HandleLevelFailed()
    {
        // commandEvent.Raise(new SetTimeScaleCommand(0f));
        // commandEvent.Raise(new PlayAudioCommand(AudioType.LevelFail));
        // commandEvent.Raise(new ShowPopupCommand(PopupType.Fail));
    }
}
