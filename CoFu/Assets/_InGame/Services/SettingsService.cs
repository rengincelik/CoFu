



using UnityEngine;

public class SettingsService : ServiceBase
{
    public SettingsChangedEventSO settingsChangedEventSO;

    public void SetMusicVolume(float volume) { /* ... */ }
    public void SetSFXVolume(float volume) { /* ... */ }
    public void SetLanguage(string languageCode) { /* ... */ }

    protected override void OnEventRaised()
    {
        Debug.Log("settings event dinlendi");
    }
}
