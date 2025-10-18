// GameButton.cs
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;

    [Header("Command")]
    [SerializeField] private CommandEventSO commandEvent;

    [SerializeField] private GameState gameState;

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
        return CommandFactory.CreateGameStateCommand(gameState);
    }
}


