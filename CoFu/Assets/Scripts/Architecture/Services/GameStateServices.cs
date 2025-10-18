// GameStateService.cs
using UnityEngine;
// GameStateService.cs - SADECE data yönetir
public class GameStateService : MonoBehaviour
{
    [SerializeField] private GameEventSO gameStateChangedEvent;

    private GameState _currentState;

    public void SetGameState(GameState newState)
    {
        _currentState = newState;
        gameStateChangedEvent?.Raise(); // ✅ Sadece "değişti" der
    }

    // Read-only
    public GameState GetGameState() => _currentState;
    public bool IsPaused() => _currentState == GameState.Paused;
}

// public class GameStateService : MonoBehaviour
// {
//     [Header("Events")]
//     [SerializeField] private GameEventSO gameStateChangedEvent;
//     [SerializeField] private GameEventSO onPausedEvent;
//     [SerializeField] private GameEventSO onResumedEvent;
//     [SerializeField] private GameEventSO onLevelCompleteEvent;
//     [SerializeField] private GameEventSO onLevelFailedEvent;

//     private GameState _currentState;

//     public void SetGameState(GameState newState)
//     {
//         GameState oldState = _currentState;
//         _currentState = newState;

//         // Genel event
//         gameStateChangedEvent?.Raise();

//         // State-spesifik event'ler (command'lar dinleyecek)
//         switch (newState)
//         {
//             case GameState.Playing:
//                 onResumedEvent?.Raise(); // ❌ Instance yok!
//                 break;

//             case GameState.Paused:
//                 onPausedEvent?.Raise();
//                 break;

//             case GameState.LevelComplete:
//                 onLevelCompleteEvent?.Raise();
//                 break;

//             case GameState.LevelFailed:
//                 onLevelFailedEvent?.Raise();
//                 break;
//         }
//     }

//     // Sadece read metodlar
//     public GameState GetGameState() => _currentState;
//     public bool IsPaused() => _currentState == GameState.Paused;
// }

// // // GameStateService.cs
// // using UnityEngine;

// // public class GameStateService : MonoBehaviour
// // {
// //     public static GameStateService Instance { get; private set; }

// //     [Header("Events")]
// //     [SerializeField] private GameEventSO onGameStateChanged;
// //     [SerializeField] private GameEventSO onScoreChanged;
// //     [SerializeField] private GameEventSO onCoinsChanged;
// //     [SerializeField] private GameEventSO onTimeChanged;

// //     private GameStateData _data = new GameStateData();

// //     void Awake()
// //     {
// //         if (Instance != null && Instance != this)
// //         {
// //             Destroy(gameObject);
// //             return;
// //         }
// //         Instance = this;
// //         DontDestroyOnLoad(gameObject);
// //     }

// //     // Write methods - sadece buradan yazılır
// //     public void SetPaused(bool paused)
// //     {
// //         _data.isPaused = paused;
// //         onGameStateChanged?.Raise();
// //     }

// //     public void AddScore(int amount)
// //     {
// //         _data.score += amount;
// //         onScoreChanged?.Raise();
// //         onGameStateChanged?.Raise();
// //     }

// //     public void AddCoins(int amount)
// //     {
// //         _data.coins += amount;
// //         onCoinsChanged?.Raise();
// //         onGameStateChanged?.Raise();
// //     }

// //     public void SetTime(float time)
// //     {
// //         _data.timeLeft = time;
// //         onTimeChanged?.Raise();
// //     }

// //     public void UseJoker()
// //     {
// //         if (_data.jokersRemaining > 0)
// //         {
// //             _data.jokersRemaining--;
// //             onGameStateChanged?.Raise();
// //         }
// //     }

// //     // Read methods - herkes okuyabilir
// //     public GameStateData GetData() => _data;
// //     public int GetScore() => _data.score;
// //     public int GetCoins() => _data.coins;
// //     public float GetTime() => _data.timeLeft;
// //     public bool IsPaused() => _data.isPaused;
// //     public int GetJokersRemaining() => _data.jokersRemaining;

// //     // Save/Load
// //     public void SaveGame()
// //     {
// //         PlayerPrefs.SetString("GameState", _data.ToJson());
// //         PlayerPrefs.Save();
// //     }

// //     public void LoadGame()
// //     {
// //         string json = PlayerPrefs.GetString("GameState", "");
// //         if (!string.IsNullOrEmpty(json))
// //         {
// //             _data = GameStateData.FromJson(json);
// //             onGameStateChanged?.Raise();
// //         }
// //     }

// //     public void ResetGame()
// //     {
// //         _data = new GameStateData();
// //         onGameStateChanged?.Raise();
// //     }
// // }
