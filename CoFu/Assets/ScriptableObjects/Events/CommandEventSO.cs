// CommandEventSO.cs
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Command Event")]
public class CommandEventSO : ScriptableObject
{
    private event Action<ICommand> _onRaised;

    public void AddListener(Action<ICommand> listener)
    {
        _onRaised += listener;
    }

    public void RemoveListener(Action<ICommand> listener)
    {
        _onRaised -= listener;
    }

    public void Raise(ICommand command)
    {
        _onRaised?.Invoke(command);
    }
}
