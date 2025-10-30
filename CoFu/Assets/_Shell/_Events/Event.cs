// // ========== EVENTS (Base) ==========

using System;
using DG.Tweening;
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

// // ========== EVENTS (Concrete) ==========


// Concrete event
[CreateAssetMenu(menuName = "Events/UseCaseEvent")]
public class UseCaseEventSO : BaseEventSO<IUseCase>
{
    public void RaiseExecute(IUseCase useCase)
    {
        // Raise event synchronously
        Raise(useCase);

        // Fire-and-forget async execution
        _ = ExecuteUseCaseAsync(useCase);
    }

    private async UniTask ExecuteUseCaseAsync(IUseCase useCase)
    {
        try
        {
            await useCase.Execute();
        }
        catch (Exception ex)
        {
            Debug.LogError($"UseCase execution failed: {ex}");
        }
    }
    
}


[CreateAssetMenu(menuName = "Events/Coin Changed Event")]
public class CurrencyChangedEventSO : BaseEventSO { }

[CreateAssetMenu(menuName = "Events/Live Changed Event")]
public class LifeChangedEventSO : BaseEventSO { }

[CreateAssetMenu(menuName = "Events/Settings Changed")]
public class SettingsChangedEventSO : BaseEventSO { }



// [CreateAssetMenu(menuName = "Events/Ad Event")]
// public class AdShownedEventSO : BaseEventSO<AdType> { }




