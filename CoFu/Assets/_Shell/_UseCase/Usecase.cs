using System;
using System.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;


public enum AdType { Rewarded, Interstitial, Banner }

// ========== INTERFACE ==========
public interface IUseCase { void Execute(); }

// ========== COMMANDS ==========
public class GameOpenUseCase : IUseCase
{
    public void Execute()
    {
        _ = GameStartAsync();
        

    }
    private async Task GameStartAsync()
    {
        AudioListenerManager.Instance.PlayMusic(MusicType.NonGamePlay);
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Loading);
        AudioListenerManager.Instance.PlayMusic(MusicType.NonGamePlay);
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Menu);
    }

}

public class LevelStartUseCase : IUseCase
{
    public void Execute()
    {
        _ = GoToPlayAsync();
        AudioListenerManager.Instance.PlayMusic(MusicType.GamePlay);
    }
    private async Task GoToPlayAsync()
    {
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }

}

public class LevelCompletedUseCase : IUseCase
{
    public void Execute()
    {
        _ = GoToPlayAsync();
    }
    private async Task GoToPlayAsync()
    {
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }
}


// public class UseJokerCommand : IUseCase
// {
//     JokerType _jokerType;
//     JokerEventSO _jokerEvent;
    
//     public UseJokerCommand(JokerType jokerType, JokerEventSO jokerEvent)
//     {
//         _jokerType = jokerType;
//         _jokerEvent = jokerEvent;
//     }
    
//     public void Execute()
//     {
//         _jokerEvent?.Raise(); // JokerManager dinler, coin kontrol + kullan
//     }
// }

// public class ShowAdCommand : IUseCase
// {
//     AdType _adType;
//     AdEventSO _adEvent;

//     public ShowAdCommand(AdType adType, AdEventSO adEvent)
//     {
//         _adType = adType;
//         _adEvent = adEvent;
//     }

//     public void Execute()
//     {
//         _adEvent.Raise(_adType);
//     }
// }

public class GameOpenCommand : IUseCase
{
    //bunu belki otomatik yaparım commande gerek olmaz ama şimdilik dursun.

    public void Execute()
    {
        //loading seq oynatılcak,
        //sonra ekran değişcek menü opening seq oynatılcak
        //opening ve menu musikleri çalabilir aynı da olabilir.
        //oyuncu play butonun atıklanana kadar beklenecek
        throw new NotImplementedException();
    }
}

public class PlayButtonClickedUseCase : IUseCase
{
    public void Execute()
    {
        LifeManager.Instance.TrySpendLife();
        IUseCase useCase = new LevelStartUseCase();
        useCase.Execute();
    }

    
}

public class NextButtonClickedUseCase : IUseCase
{
    public void Execute()
    {

        _ = GoToPlayAsync();
    }
    private async Task GoToPlayAsync()
    {
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }

}

public class RetryButtonClickedUseCase : IUseCase
{
    public void Execute()
    {
        //start için can gitcek
        
        _ = GoToPlayAsync();
    }
    private async Task GoToPlayAsync()
    {
        await PopupManager.Instance.ClosePopupAsync();
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }

}

