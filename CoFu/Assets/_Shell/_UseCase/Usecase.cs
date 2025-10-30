using System;
using System.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;


public enum AdType { Rewarded, Interstitial, Banner }

public interface IUseCase { UniTask Execute(); }

public class GameOpenUseCase : IUseCase
{
    public async UniTask Execute()
    {
        Debug.Log("GameOpenUseCase");
        // AudioListenerManager.Instance.PlayMusic(MusicType.NonGamePlay);
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Loading);
        // AudioListenerManager.Instance.PlayMusic(MusicType.NonGamePlay);
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Menu);

    }

}

public class LevelStartUseCase : IUseCase
{
    public async UniTask Execute()
    {
        Debug.Log("LevelStartUseCase");
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
        // AudioListenerManager.Instance.PlayMusic(MusicType.GamePlay);
        GridManager.Instance.GeneratePlayGroundGrid();
    }

}

public class LevelWonUseCase : IUseCase
{
    public async UniTask Execute()
    {
        await PopupManager.Instance.OpenPopupAsync(PopupType.Win);
    }
      
}

public class LevelFailedUseCase : IUseCase
{
    public async UniTask Execute()
    {
        await PopupManager.Instance.OpenPopupAsync(PopupType.Fail);
    }
}

// public class PlayButtonClickedUseCase : IUseCase
// {
//     public async UniTask Execute()
//     {
//         LifeManager.Instance.TrySpendLife();
//         IUseCase useCase = new LevelStartUseCase();
//         await useCase.Execute();
//     }

    
// }

// public class NextButtonClickedUseCase : IUseCase
// {
//     public async UniTask Execute()
//     {
//         await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
//     }

// }

// public class RetryButtonClickedUseCase : IUseCase
// {
//     public async UniTask Execute()
//     {
//         //start için can gitcek
//         await PopupManager.Instance.ClosePopupAsync();
//         await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
//     }

// }

