
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] public List<ScreenView> screenViews;

    public void GoToLayer(ScreenViewType type)
    {
        foreach(var s in screenViews)
        {
            s.gameObject.SetActive(s.type == type);
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

