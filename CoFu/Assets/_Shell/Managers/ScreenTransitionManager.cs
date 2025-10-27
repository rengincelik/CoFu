using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] private List<ScreenView> screenViews;
    private ScreenView currentScreen;
    [SerializeField] bool isStart = false;


    private async void StartGame()
    {
        await GoToLayerAsync(ScreenViewType.Loading);
        await GoToLayerAsync(ScreenViewType.Menu);
    }

    public async Task GoToLayerAsync(ScreenViewType type)
    {
        await OutOfCurrentLayerAsync();

        if (screenViews == null || screenViews.Count == 0)
        {
            Debug.LogWarning("[ScreenManager] screenViews list is null or empty.");
            return;
        }

        foreach (var s in screenViews)
        {
            if (s == null) continue;

            bool active = s.type == type;
            s.gameObject.SetActive(active);

            if (active)
            {
                // 1. Önce objeyi aktif et ama tüm öğeleri başlangıç state'ine getir
                foreach (var item in s.openingSequences)
                {
                    TweenFactory.PrepareInitialState(item.gameObject, item.animation);
                }

                // 2. Animasyonu oynat ve bitmesini bekle
                await SequenceService.PlaySequenceAsync(s.openingSequences);

                currentScreen = s;
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        }
    }

    public async Task OutOfCurrentLayerAsync()
    {
        if (currentScreen == null) return;

        if (currentScreen.closingSequences != null && currentScreen.closingSequences.Length > 0)
        {
            await SequenceService.PlaySequenceAsync(currentScreen.closingSequences);
        }

        currentScreen.gameObject.SetActive(false);
        currentScreen = null;
    }

    [ContextMenu("start the game")]
    public void StartTheGame() =>  StartGame();


    [ContextMenu("Go To Layer Play")]
    public void GoToLayerPlay() => _ = GoToLayerAsync(ScreenViewType.Play);

    [ContextMenu("Go To Layer Loading")]
    public void GoToLayerLoading() => _ = GoToLayerAsync(ScreenViewType.Loading);

    [ContextMenu("Go To Layer Menu")]
    public void GoToLayerMenu() => _ = GoToLayerAsync(ScreenViewType.Menu);

}


