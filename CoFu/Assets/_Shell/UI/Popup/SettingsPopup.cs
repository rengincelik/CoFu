// SettingsPopup.cs
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : Popup
{
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle vibrationToggle;
    
    protected  void OnShown()
    {
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;
        vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
    }
}


