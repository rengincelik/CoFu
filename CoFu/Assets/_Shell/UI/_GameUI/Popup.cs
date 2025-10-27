using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum PopupType { Pause, Win, Fail, Settings, NoLive, LowCoin }

public class Popup : MonoBehaviour
{
    public PopupType popupType;
    public AudioClip audioClip;
    public AnimationSequenceItem[] openingSequences;
    public AnimationSequenceItem[] closingSequences;


    
}

