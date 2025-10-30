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
    // [SerializeField] private List<UnityEngine.UI.Button> buttons;

    // public void SetButtonsInteractable(bool value)
    // {
    //     foreach (var b in buttons)
    //         if(b != null) b.interactable = value;
    // }

}

