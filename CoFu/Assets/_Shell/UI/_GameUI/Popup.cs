using System.Collections.Generic;
using UnityEngine;

public enum PopupType { Pause, Win, Fail, Settings, NoLive, LowCoin }

public class Popup : MonoBehaviour
{
    public PopupType popupType;
    public AudioClip openingAudioClip;
    public AudioClip closingAudioClip;
    public AnimationSequenceItem[] openingSequences;
    public AnimationSequenceItem[] closingSequences;

    public void Open()
    {
        gameObject.SetActive(true);
        // Animasyon çalıştır
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

