
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : ScreenView
{
    // [SerializeField] SFXEventSO sFXEventSO;
    [SerializeField] AudioClip clip;
    [SerializeField] private Button playButton;


    void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }


    void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayClicked);
    }


    // wrapper metod
    private void OnPlayClicked()
    {
        // sFXEventSO?.Raise(clip);
        AudioListenerManager.Instance.PlaySFX(clip);
        _ = GoToPlayAsync(); // async Task metodunu çağır
    }


    // async metod
    private async Task GoToPlayAsync()
    {
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }

}
