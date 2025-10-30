using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private List<Popup> popups;

    private Popup currentPopup;

    public Popup GetPopup(PopupType popupType)
    {
        Debug.Log($"[GetPopup] Looking for {popupType}, Total popups: {popups?.Count ?? 0}");
    
        foreach (var p in popups)
        {
            if (p.popupType == popupType) return p;
        }
        return null;
    }
    public async UniTask OpenPopupAsync(PopupType type)
    {

        if (currentPopup != null) return;

        Popup p = GetPopup(type);
        Debug.Log($"[OpenPopup] Type: {type}, Found popup: {(p != null ? p.name : "NULL")}");
    

        if (p == null) return;

        bool active = p.popupType == type;
        p.gameObject.SetActive(active);

        if (active)
        {
            foreach (var item in p.openingSequences)
            {
                TweenFactory.PrepareInitialState(item.gameObjects, item.animations);
            }

            await SequenceService.PlaySequenceAsync(p.openingSequences);

            currentPopup = p;
        }
            

        

    }

    public async UniTask ClosePopupAsync()
    {
        if (currentPopup == null) return;

        if (currentPopup.closingSequences != null)
        {
            await SequenceService.PlaySequenceAsync(currentPopup.closingSequences);
        }

        currentPopup.gameObject.SetActive(false);
        currentPopup = null;
    }
    
    [ContextMenu("Go To Popup Win")]
    public void GoToPopupWin() => _ = OpenPopupAsync(PopupType.Win);
    [ContextMenu("Go To Popup Fail")]
    public void GoToPopupFail() => _ = OpenPopupAsync(PopupType.Fail);
    [ContextMenu("Go To Popup Settings")]
    public void GoToPopupSettings() => _ = OpenPopupAsync(PopupType.Settings);

    [ContextMenu("Close Popup")]
    public void ClosePopup() => _ = ClosePopupAsync();

}


