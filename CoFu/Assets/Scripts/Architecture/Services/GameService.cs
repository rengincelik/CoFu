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

public class GameService : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

