// CommandExecutor.cs
using UnityEngine;
using System.Collections.Generic;

public class CommandExecutor : MonoBehaviour
{
    public static CommandExecutor Instance { get; private set; }

    [Header("Events")]
    [SerializeField] private CommandEventSO commandEvent;


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

        // Analytics
        Debug.Log($"[CommandExecutor] Executed: {command.GetType().Name}");
    }

}
