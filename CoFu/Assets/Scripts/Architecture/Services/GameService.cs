// Sorumluluklar:
// ├── Oyun döngüsü yönetimi (Start, Pause, GameOver)
// ├── GameState geçişleri (Menu → Playing → GameOver)
// ├── Sahne yükleme koordinasyonu
// └── Global event broadcasting

// Bağımlılıklar:
// ├── LevelService (level bilgisi için)
// ├── EconomyService (puan/para için)
// └── PlayerData (kayıt için)

// Metodlar:
// ├── StartGame(int levelIndex)
// ├── PauseGame()
// ├── ResumeGame()
// ├── EndGame(bool victory)
// └── QuitToMenu()

using UnityEngine;
using UnityEngine.SceneManagement;

// public class GameService : Singleton<GameService> {
//     public GameState CurrentState { get; private set; }

//     // Events
//     public event System.Action OnGameStarted;
//     public event System.Action OnGamePaused;
//     public event System.Action OnGameResumed;
//     public event System.Action<bool> OnGameEnded; // bool = isWin

//     // ============================================
//     // GAME FLOW
//     // ============================================
//     public void StartGame() {
//         CurrentState = GameState.Playing;
//         Time.timeScale = 1f;
//         OnGameStarted?.Invoke();

//         Debug.Log("[Game] Started");
//     }

//     public void PauseGame() {
//         if (CurrentState != GameState.Playing) {
//             return; // Zaten duraklatılmış
//         }

//         CurrentState = GameState.Paused;
//         Time.timeScale = 0f;
//         OnGamePaused?.Invoke();

//         Debug.Log("[Game] Paused");
//     }

//     public void ResumeGame() {
//         if (CurrentState != GameState.Paused) {
//             return; // Pause'da değilse resume edilemez
//         }

//         CurrentState = GameState.Playing;
//         Time.timeScale = 1f;
//         OnGameResumed?.Invoke();

//         Debug.Log("[Game] Resumed");
//     }

//     public void EndGame(bool isWin) {
//         CurrentState = isWin ? GameState.Won : GameState.Lost;
//         Time.timeScale = 1f; // End screen animasyonları için
//         OnGameEnded?.Invoke(isWin);

//         Debug.Log($"[Game] Ended - {(isWin ? "WIN" : "LOSE")}");
//     }

//     public void RestartLevel() {
//         // Mevcut level'i yeniden başlat
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }

//     public void ReturnToMenu() {
//         Time.timeScale = 1f;
//         CurrentState = GameState.MainMenu;
//         SceneManager.LoadScene("MainMenu");
//     }

//     // ============================================
//     // MOBILE HELPERS
//     // ============================================
//     public bool CanPause() {
//         return CurrentState == GameState.Playing;
//     }

//     public bool CanResume() {
//         return CurrentState == GameState.Paused;
//     }

//     public bool IsPlaying() {
//         return CurrentState == GameState.Playing;
//     }
// }

// ============================================
// ÖRNEK 3: Game Service - NORMAL MonoBehaviour
// (Event-based, Update'e gerek yok)
// ============================================
public class GameService : Singleton<GameService>
{
    public GameState CurrentState { get; private set; }


    public void StartGame()
    {
        
    }

    public void PauseGame()
    {
        
    }

    public void ResumeGame()
    {

    }
    
}
