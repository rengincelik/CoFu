// CommandExecutor.cs
using UnityEngine;
using System.Collections.Generic;

public class CommandExecutor : MonoBehaviour
{
    public static CommandExecutor Instance { get; private set; }

    [Header("Events")]
    [SerializeField] private CommandEventSO commandEvent;

    // Undo/Redo için
    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        commandEvent.AddListener(ExecuteCommand);
    }

    void OnDisable()
    {
        commandEvent.RemoveListener(ExecuteCommand);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);

        // Analytics
        Debug.Log($"[CommandExecutor] Executed: {command.GetType().Name}");
    }

    // public void UndoLastCommand()
    // {
    //     if (_commandHistory.Count > 0)
    //     {
    //         ICommand lastCommand = _commandHistory.Pop();
    //         lastCommand.Undo();
    //         Debug.Log($"[CommandExecutor] Undid: {lastCommand.GetType().Name}");
    //     }
    // }
}
