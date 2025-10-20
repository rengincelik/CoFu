using System.Collections.Generic;
using UnityEngine;

public enum PopupType { Pause, Win, Fail, Settings, NoLive, LowCoin }
[System.Serializable]
public struct PopupShow
{
    public AnimationType animation;
    public float transitionDuration;
}
[System.Serializable]
public struct PopupClose
{
    public AnimationType animation;
    public float transitionDuration;
}

public class Popup : MonoBehaviour
{
    public PopupType popupType;
    public List<PopupItem> popupItems;

}

