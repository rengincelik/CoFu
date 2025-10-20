
using DG.Tweening;
using UnityEngine;

public class AudioService : Singleton<AudioService>
{
    [SerializeField] AudioEventSO audioEvent;
    [SerializeField] SFXEventSO sfxEvent;

    [SerializeField] AudioSource musicSource; // Loop için
    [SerializeField] AudioSource sfxSource; // OneShot için

    void OnEnable()
    {
        // audioEvent.AddListener(PlayMusic);
        // sfxEvent.AddListener(PlaySFX);
    }

    void PlayMusic(MusicType type)
    {
        // AudioClip clip = GetMusicClip(type);

        // // Cross-fade
        // musicSource.DOFade(0, 0.5f).OnComplete(() =>
        // {
        //     musicSource.clip = clip;
        //     musicSource.Play();
        //     musicSource.DOFade(1, 0.5f);
        // });
    }

    void PlaySFX(SFXType type)
    {
        // AudioClip clip = GetSFXClip(type);
        // sfxSource.PlayOneShot(clip);
    }
}

