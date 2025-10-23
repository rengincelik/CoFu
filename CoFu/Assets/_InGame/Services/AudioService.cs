
using DG.Tweening;
using UnityEngine;

public class AudioService : ServiceBase<MusicType,bool>
{
    [SerializeField] AudioSource musicSource; // Loop için
    [SerializeField] AudioSource sfxSource; // OneShot için

    

    protected override void OnEventRaised(MusicType value1, bool value2)
    {
        Debug.Log("audio event Dinlendi");
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

