// AddLivePopup.cs
using TMPro;
using UnityEngine;

public class AddLivePopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI messageText;
    
    protected override void OnShown()
    {
        messageText.text = "You're out of lives!\nWatch an ad to continue";
    }
}
