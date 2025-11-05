
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class AudioConfig
{
    public MusicType musicType;
}
public enum MusicType { GamePlay, NonGamePlay ,ButtonClick, Victory, Fail, CoinCollect}
public class AudioListenerManager : Singleton<AudioListenerManager>
{
    
    //buraya muhtemelen settingslerden volume ayarlama ve susturma kısımlarınında dinlenecek.
    //unutma
    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioClip[] sfxClip;
    [SerializeField] AudioSource musicSource; // Loop için
    [SerializeField] AudioSource sfxSource; // OneShot için




    public void PlayMusic(MusicType musicType)
    {
        musicSource.DOFade(0f, 0.5f).OnComplete(() =>
        {
            // musicSource.clip = clip;
            musicSource.Play();
            musicSource.DOFade(1f, 0.5f);
        });


    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

}


