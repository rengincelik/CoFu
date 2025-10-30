
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : ScreenView
{
    
    [SerializeField] UseCaseEventSO useCaseEventSO;
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


    private void OnPlayClicked()
    {
        IUseCase useCase = new LevelStartUseCase();
        useCaseEventSO.RaiseExecute(useCase);
        
    }


}
