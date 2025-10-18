// GameStateService.cs
using UnityEngine;

public class GameStateService : MonoBehaviour
{
    public static GameStateService Instance { get; private set; }

    [Header("Events")]
    [SerializeField] private GameEventSO onGameStateChanged;
    [SerializeField] private GameEventSO onScoreChanged;
    [SerializeField] private GameEventSO onCoinsChanged;
    [SerializeField] private GameEventSO onTimeChanged;

    private GameStateData _data = new GameStateData();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Write methods - sadece buradan yazılır
    public void SetPaused(bool paused)
    {
        _data.isPaused = paused;
        onGameStateChanged?.Raise();
    }

    public void AddScore(int amount)
    {
        _data.score += amount;
        onScoreChanged?.Raise();
        onGameStateChanged?.Raise();
    }

    public void AddCoins(int amount)
    {
        _data.coins += amount;
        onCoinsChanged?.Raise();
        onGameStateChanged?.Raise();
    }

    public void SetTime(float time)
    {
        _data.timeLeft = time;
        onTimeChanged?.Raise();
    }

    public void UseJoker()
    {
        if (_data.jokersRemaining > 0)
        {
            _data.jokersRemaining--;
            onGameStateChanged?.Raise();
        }
    }

    // Read methods - herkes okuyabilir
    public GameStateData GetData() => _data;
    public int GetScore() => _data.score;
    public int GetCoins() => _data.coins;
    public float GetTime() => _data.timeLeft;
    public bool IsPaused() => _data.isPaused;
    public int GetJokersRemaining() => _data.jokersRemaining;

    // Save/Load
    public void SaveGame()
    {
        PlayerPrefs.SetString("GameState", _data.ToJson());
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        string json = PlayerPrefs.GetString("GameState", "");
        if (!string.IsNullOrEmpty(json))
        {
            _data = GameStateData.FromJson(json);
            onGameStateChanged?.Raise();
        }
    }

    public void ResetGame()
    {
        _data = new GameStateData();
        onGameStateChanged?.Raise();
    }
}
