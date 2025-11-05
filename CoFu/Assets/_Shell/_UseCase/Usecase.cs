using System;
using System.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using Cysharp.Threading.Tasks;



public interface IUseCase { UniTask Execute(); }

public class GameOpenUseCase : IUseCase
{
    AudioChangedEventSO audioChangedEventSO;
    
    public async UniTask Execute()
    {
        Debug.Log("GameOpenUseCase");
        //audio ile ilgili birşey ekle
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Loading);
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Menu);

    }

}

public class LevelStartUseCase : IUseCase
{
    public async UniTask Execute()
    {
        Debug.Log("LevelStartUseCase");
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
        
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


