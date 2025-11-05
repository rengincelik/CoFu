// // ========== EVENTS (Base) ==========

using System;
using UnityEngine;
using Cysharp.Threading.Tasks;




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



[CreateAssetMenu(menuName = "Events/Audio Changed")]
public class AudioChangedEventSO : BaseEventSO<AudioConfig> { }




