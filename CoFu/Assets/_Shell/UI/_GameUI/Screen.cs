using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum ScreenViewType {Play,Menu,Loading}
public class ScreenView : MonoBehaviour
{
    public ScreenViewType type;
    public AudioClip audioClip;
    public AnimationSequenceItem[] openingSequences;
    public AnimationSequenceItem[] closingSequences;
    

}

