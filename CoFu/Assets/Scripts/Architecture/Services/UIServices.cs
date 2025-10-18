// UIService.cs
using UnityEngine;
using DG.Tweening;

public class UIService : MonoBehaviour
{
    public static UIService Instance { get; private set; }

    [Header("Views")]
    [SerializeField] private GameObject mainMenuView;
    [SerializeField] private GameObject gameView;

    [Header("Popups - Pool")]
    [SerializeField] private PausePopup pausePopupPrefab;
    [SerializeField] private WinPopup winPopupPrefab;
    [SerializeField] private FailPopup failPopupPrefab;

    private PausePopup _pausePopup;
    private WinPopup _winPopup;
    private FailPopup _failPopup;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePopups();
    }

    void InitializePopups()
    {
        // Popup pool - başta oluştur, gizle
        _pausePopup = Instantiate(pausePopupPrefab, transform);
        _pausePopup.gameObject.SetActive(false);

        _winPopup = Instantiate(winPopupPrefab, transform);
        _winPopup.gameObject.SetActive(false);

        _failPopup = Instantiate(failPopupPrefab, transform);
        _failPopup.gameObject.SetActive(false);
    }

    // View Switching
    public void ShowMainMenu()
    {
        SwitchView(mainMenuView, gameView);
    }

    public void ShowGameView()
    {
        SwitchView(gameView, mainMenuView);
    }

    private void SwitchView(GameObject show, GameObject hide)
    {
        CanvasGroup hideGroup = hide.GetComponent<CanvasGroup>();
        CanvasGroup showGroup = show.GetComponent<CanvasGroup>();

        hideGroup.DOFade(0, 0.3f).OnComplete(() => hide.SetActive(false));

        show.SetActive(true);
        showGroup.alpha = 0;
        showGroup.DOFade(1, 0.3f);
    }

    // Popup Management
    public void ShowPausePopup() => _pausePopup.Show();
    public void HidePausePopup() => _pausePopup.Hide();

    public void ShowWinPopup() => _winPopup.Show();
    public void ShowFailPopup() => _failPopup.Show();
}
