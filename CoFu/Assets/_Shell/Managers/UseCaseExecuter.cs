using Cysharp.Threading.Tasks;
using UnityEngine;

public class UseCaseExecuter : MonoBehaviour
{
    [SerializeField] private UseCaseEventSO useCaseEventSO;

    private void OnEnable()
    {
        if (useCaseEventSO != null)
            useCaseEventSO.AddListener(OnUseCaseRaised);
    }

    private void OnDisable()
    {
        if (useCaseEventSO != null)
            useCaseEventSO.RemoveListener(OnUseCaseRaised);
    }

    private void OnUseCaseRaised(IUseCase useCase)
    {
        // Fire-and-forget UniTask ile Execute
        Debug.Log("use case executed async");
        _ = ExecuteUseCaseAsync(useCase);
    }

    private async UniTask ExecuteUseCaseAsync(IUseCase useCase)
    {
        try
        {
            await useCase.Execute();
            Debug.Log("use case executed async");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"UseCase execution failed: {ex}");
        }
    }


    [ContextMenu("win")]
    public void LevelWon()
    {
        useCaseEventSO.RaiseExecute(new LevelWonUseCase());
    }
    [ContextMenu("fail")]
    public void LevelFailed()
    {
        useCaseEventSO.RaiseExecute(new LevelFailedUseCase());
    }

}
