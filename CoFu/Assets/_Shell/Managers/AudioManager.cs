
using DG.Tweening;
using UnityEngine;

public class AudioListenerManager:MonoBehaviour
{
    //buraya muhtemelen settingslerden volume ayarlama ve susturma kısımlarınında dinlenecek.
    //unutma
    
    [SerializeField] AudioSource musicSource; // Loop için
    [SerializeField] AudioSource sfxSource; // OneShot için

    [SerializeField] MusicEventSO musicEventSO;
    [SerializeField] SFXEventSO sFXEventSO;

    void OnEnable()
    {
        musicEventSO?.AddListener(PlayMusic);
        sFXEventSO?.AddListener(PlaySFX);
    }

    void OnDisable()
    {
        musicEventSO?.RemoveListener(PlayMusic);
        sFXEventSO?.RemoveListener(PlaySFX);
    }





    void PlayMusic(AudioClip clip, bool isOn)
    {
        musicSource.DOFade(0f, 0.5f).OnComplete(() =>
        {
            musicSource.clip = clip;
            musicSource.Play();
            musicSource.DOFade(1f, 0.5f);
        });
    

    }

    void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

}

