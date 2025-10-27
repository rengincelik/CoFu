
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : ScreenView
{
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
        _ = GoToPlayAsync(); // async Task metodunu çağır
    }


    // async metod
    private async Task GoToPlayAsync()
    {
        await ScreenManager.Instance.GoToLayerAsync(ScreenViewType.Play);
    }

}
