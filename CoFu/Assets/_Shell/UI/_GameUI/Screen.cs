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
    

    
}

