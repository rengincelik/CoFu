using System.Collections.Generic;
using UnityEngine;

public enum ScreenViewType {Play,Manu,Loading}
public class ScreenView : MonoBehaviour
{
    public ScreenViewType type;
    public List<Popup> popups;
    public List<ScreenItem> screenItems;

}

