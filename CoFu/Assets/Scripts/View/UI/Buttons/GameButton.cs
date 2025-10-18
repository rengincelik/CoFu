// GameButton.cs
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;
    
    [Header("Command")]
    [SerializeField] private CommandEventSO commandEvent;
    [SerializeField] private CommandType commandType;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ICommand command = CreateCommand();
        if (command != null)
        {
            commandEvent.Raise(command);
        }
    }

    private ICommand CreateCommand()
    {
        return commandType switch
        {
            CommandType.Pause => new PauseCommand(),
            CommandType.Resume => new ResumeCommand(),
            CommandType.Play => new PlayCommand(),
            CommandType.Restart => new RestartCommand(),
            // Daha fazla ekle...
            _ => null
        };
    }
}

// Enum - Inspector'da seç
public enum CommandType
{
    Pause,
    Resume,
    Play,
    Restart,
    NextLevel,
    UseJoker,
    WatchAd
}

