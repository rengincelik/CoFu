
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] public List<ScreenView> screenViews;

    public void GoToLayer(ScreenViewType type)
    {
        if (screenViews == null || screenViews.Count == 0)
        {
            Debug.LogWarning("[ScreenManager] screenViews list is null or empty.");
            return;
        }

        foreach (var s in screenViews)
        {
            if (s == null)
                continue;

            bool active = s.type == type;
            s.gameObject.SetActive(active);

            if (active && s.openingSequences != null && s.openingSequences.Length > 0)
            {
                if (SequenceService.Instance != null)
                    SequenceService.Instance.PlaySequence(s.openingSequences);
                else
                    Debug.LogWarning("[ScreenManager] SequenceService instance is null.");
            }
        }
    }




    [ContextMenu("Go To Layer Play")]
    public void GoToLayerPlay()
    {
        GoToLayer(ScreenViewType.Play);
    }

    [ContextMenu("Go To Layer Loading")]
    public void GoToLayerLoading()
    {
        GoToLayer(ScreenViewType.Loading);
    }



}

