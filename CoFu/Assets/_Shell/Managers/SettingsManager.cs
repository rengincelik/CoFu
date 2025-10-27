



using UnityEngine;

public class SettingsManager
{
    //burda çok iş var.
    //uı bağlantısı lazım
    //eventin içine data lazım veya başka eventler
    public SettingsChangedEventSO settingsChangedEventSO;

    public void SetMusicVolume(float volume) {
        settingsChangedEventSO?.Raise();
    }
    public void SetSFXVolume(float volume)
    {
        settingsChangedEventSO?.Raise();
    }


}
