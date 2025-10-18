using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // void Start()
    // {
    //     musicSlider.value = PlayerData.Instance.MusicVolume;
    //     sfxSlider.value = PlayerData.Instance.SFXVolume;

    //     musicSlider.onValueChanged.AddListener(OnMusicChanged);
    //     sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    // }

    void OnMusicChanged(float value)
    {
        PlayerData.Instance.SetMusicVolume(value);
    }

    void OnSFXChanged(float value)
    {
        PlayerData.Instance.SetSFXVolume(value);
    }
}

