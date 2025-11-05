
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class BaseEventSO : ScriptableObject
{
    private event Action _onRaised;
    public void AddListener(Action listener) => _onRaised += listener;
    public void RemoveListener(Action listener) => _onRaised -= listener;
    public void Raise() => _onRaised?.Invoke();
}

public abstract class BaseEventSO<T> : ScriptableObject
{
    private event Action<T> _onRaised;
    public void AddListener(Action<T> listener) => _onRaised += listener;
    public void RemoveListener(Action<T> listener) => _onRaised -= listener;
    public void Raise(T value) => _onRaised?.Invoke(value);
}

public abstract class BaseEventSO<T1, T2> : ScriptableObject
{
    private event Action<T1, T2> _onRaised;
    public void AddListener(Action<T1, T2> listener) => _onRaised += listener;
    public void RemoveListener(Action<T1, T2> listener) => _onRaised -= listener;
    public void Raise(T1 value1, T2 value2) => _onRaised?.Invoke(value1, value2);
}
