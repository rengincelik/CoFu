using System.Collections.Generic;
using UnityEngine;

public enum ScreenViewType {Play,Menu,Loading}
public class ScreenView : MonoBehaviour
{
    public ScreenViewType type;
    public AudioClip openingAudioClip;
    public AudioClip closingAudioClip;
    public AnimationSequenceItem[] openingSequences;
    public AnimationSequenceItem[] closingSequences;
    
    public List<Popup> popups;
    public List<ScreenItem> screenItems;
    public Popup GetPopup(PopupType type)
    {
        return popups.Find(p => p.popupType == type);
    }

    public void ShowPopup(PopupType type)
    {
        GetPopup(type)?.Open();
    }
    
}

