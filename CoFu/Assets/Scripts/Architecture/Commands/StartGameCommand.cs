// Tetikleme: MainMenu → "Play" butonu tıklaması
// Akış:
// 1. CanExecute() → levelIndex valid mi?
// 2. GameService.StartGame(levelIndex)
// 3. LevelService.LoadLevel(levelIndex)
// 4. Scene geçişini bekle (async)


using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCommand : ICommand {
    private LevelConfig levelToLoad;
    
    public StartGameCommand(LevelConfig level) {
        levelToLoad = level;
    }

    public bool CanExecute()
    {
        throw new System.NotImplementedException();
    }

    public void Execute() {
        // // 1. Sahneye geç
        // SceneManager.LoadScene("GamePlay");
        
        // // 2. Level'i yükle
        // LevelService.Instance.LoadLevel(levelToLoad);
        
        // // 3. Oyunu başlat
        // GameService.Instance.StartGame();
    }
}

