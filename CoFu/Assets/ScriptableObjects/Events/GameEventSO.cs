// GameEventSO.cs
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEventSO : ScriptableObject
{
    private event Action _onRaised;

    public void AddListener(Action listener)
    {
        _onRaised += listener;
    }

    public void RemoveListener(Action listener)
    {
        _onRaised -= listener;
    }

    public void Raise()
    {
        _onRaised?.Invoke();
    }
}

