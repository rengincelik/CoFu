using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] UseCaseEventSO useCaseEventSO;
    [SerializeField] private List<ScreenView> screenViews;
    private ScreenView currentScreen;


    private void Start()
    {
        useCaseEventSO.RaiseExecute(new GameOpenUseCase());

    }

    public async UniTask GoToLayerAsync(ScreenViewType type)
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
                foreach (var item in s.openingSequences)
                {
                    TweenFactory.PrepareInitialState(item.gameObjects, item.animations);
                }

                await SequenceService.PlaySequenceAsync(s.openingSequences);

                currentScreen = s;
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        }
    }

    public async UniTask OutOfCurrentLayerAsync()
    {
        if (currentScreen == null) return;

        if (currentScreen.closingSequences != null && currentScreen.closingSequences.Length > 0)
        {
            await SequenceService.PlaySequenceAsync(currentScreen.closingSequences);
        }

        currentScreen.gameObject.SetActive(false);
        currentScreen = null;
    }



    [ContextMenu("Go To Layer Play")]
    public void GoToLayerPlay() => _ = GoToLayerAsync(ScreenViewType.Play);

    [ContextMenu("Go To Layer Loading")]
    public void GoToLayerLoading() => _ = GoToLayerAsync(ScreenViewType.Loading);

    [ContextMenu("Go To Layer Menu")]
    public void GoToLayerMenu() => _ = GoToLayerAsync(ScreenViewType.Menu);

}


