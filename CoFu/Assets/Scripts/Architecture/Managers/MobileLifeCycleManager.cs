// using UnityEngine;

// public class MobileLifecycleManager : Singleton<MobileLifecycleManager> {
//     [Header("Settings")]
//     [SerializeField] private bool autoSaveOnPause = true;
//     [SerializeField] private bool autoPauseGame = true;
//     [SerializeField] private float backgroundMusicVolume = 0.3f;
    
//     // State tracking
//     private bool wasPlayingBeforePause = false;
//     private float originalMusicVolume = 1f;
    
//     // ============================================
//     // ARKA PLANA GİTTİ (Telefon, Bildirim, Ana Ekran)
//     // ============================================
//     void OnApplicationPause(bool isPaused) {
//         if (isPaused) {
//             OnGoingToBackground();
//         } else {
//             OnReturningToForeground();
//         }
//     }
    
//     // ============================================
//     // FOCUS DEĞİŞTİ (Reklam, Dialog vb.)
//     // ============================================
//     void OnApplicationFocus(bool hasFocus) {
//         if (!hasFocus) {
//             OnLostFocus();
//         } else {
//             OnGainedFocus();
//         }
//     }
    
//     // ============================================
//     // UYGULAMA KAPANDI
//     // ============================================
//     void OnApplicationQuit() {
//         OnGameClosing();
//     }
    
//     // ============================================
//     // ARKA PLANA GİTME - EN ÖNEMLİ!
//     // ============================================
//     private void OnGoingToBackground() {
//         Debug.Log("[Lifecycle] Going to background...");
        
//         // 1. Oyun oynuyorsa duraklat
//         if (autoPauseGame && GameService.Instance.CurrentState == GameState.Playing) {
//             wasPlayingBeforePause = true;
//             GameService.Instance.PauseGame();
//             Debug.Log("[Lifecycle] Game auto-paused");
//         }
        
//         // 2. Verileri kaydet (kritik!)
//         if (autoSaveOnPause) {
//             SaveGameData();
//         }
        
//         // 3. Müziği kıs (pil tasarrufu)
//         LowerBackgroundMusic();
        
//         // 4. Timer'ları durdur
//         Time.timeScale = 0f;
        
//         // 5. Network bağlantısını kes (varsa)
//         // NetworkManager.Disconnect();
        
//         // 6. Analytics gönder
//         // Analytics.SendEvent("app_backgrounded");
//     }
    
//     // ============================================
//     // ÖN PLANA DÖNME
//     // ============================================
//     private void OnReturningToForeground() {
//         Debug.Log("[Lifecycle] Returning to foreground...");
        
//         // 1. Timer'ları devam ettir
//         Time.timeScale = 1f;
        
//         // 2. Müziği normale döndür
//         RestoreBackgroundMusic();
        
//         // 3. Oyun durumunu kontrol et
//         if (wasPlayingBeforePause) {
//             // Kullanıcıya "Resume" butonu göster, otomatik başlatma!
//             // ShowResumeDialog();
//             wasPlayingBeforePause = false;
//         }
        
//         // 4. Verileri yeniden yükle (başka cihazda değişmiş olabilir)
//         // PlayerData.Instance.LoadAllData();
        
//         // 5. Network'ü yeniden bağla
//         // NetworkManager.Reconnect();
        
//         // 6. Bildirimleri temizle
//         // NotificationManager.ClearAll();
        
//         Debug.Log("[Lifecycle] Foreground restored");
//     }
    
//     // ============================================
//     // FOCUS KAYBI (Reklam, System Dialog)
//     // ============================================
//     private void OnLostFocus() {
//         Debug.Log("[Lifecycle] Lost focus");
        
//         // Oyun oynuyorsa duraklat (reklam gösterirken önemli!)
//         if (GameService.Instance.CurrentState == GameState.Playing) {
//             GameService.Instance.PauseGame();
//         }
        
//         // Sesleri kıs
//         AudioListener.pause = true;
//     }
    
//     // ============================================
//     // FOCUS KAZANMA
//     // ============================================
//     private void OnGainedFocus() {
//         Debug.Log("[Lifecycle] Gained focus");
        
//         // Sesleri aç
//         AudioListener.pause = false;
        
//         // NOT: Oyunu otomatik resume etme, kullanıcı butona bassın!
//     }
    
//     // ============================================
//     // UYGULAMA KAPANIYOR
//     // ============================================
//     private void OnGameClosing() {
//         Debug.Log("[Lifecycle] Application quitting...");
        
//         // 1. SON KEZ KAYDET (en önemli!)
//         SaveGameData();
        
//         // 2. Açık level varsa playtime kaydet
//         if (GameService.Instance.CurrentState == GameState.Playing) {
//             float sessionTime = Time.time; // veya LevelService'ten al
//             PlayerData.Instance.AddPlaytime(sessionTime);
//         }
        
//         // 3. Analytics gönder
//         // Analytics.SendEvent("app_closed", new Dictionary<string, object> {
//         //     {"session_duration", Time.time},
//         //     {"last_level", LevelService.Instance.currentLevel?.levelNumber}
//         // });
        
//         // 4. Kaynakları temizle
//         // Resources.UnloadUnusedAssets();
        
//         Debug.Log("[Lifecycle] Quit complete");
//     }
    
//     // ============================================
//     // HELPER METHODS
//     // ============================================
//     private void SaveGameData() {
//         Debug.Log("[Lifecycle] Saving game data...");
        
//         // Oyun ortasında kaydediliyorsa level durumunu kaydet
//         if (GameService.Instance.CurrentState == GameState.Playing) {
//             // Level progress kaydet
//             // int currentLevelNumber = LevelService.Instance.currentLevel.levelNumber;
//             // float remainingTime = LevelService.Instance.RemainingTime;
//             // int collectedEssence = LevelService.Instance.CollectedCount;
            
//             // PlayerPrefs.SetInt("LastLevel", currentLevelNumber);
//             // PlayerPrefs.SetFloat("LastLevelTime", remainingTime);
//             // PlayerPrefs.SetInt("LastLevelProgress", collectedEssence);
//         }
        
//         // PlayerData zaten auto-save yapıyor ama emin olmak için:
//         PlayerPrefs.Save();
        
//         Debug.Log("[Lifecycle] Save complete");
//     }
    
//     private void LowerBackgroundMusic() {
//         // AudioManager varsa kullan
//         // originalMusicVolume = AudioManager.Instance.MusicVolume;
//         // AudioManager.Instance.SetMusicVolume(backgroundMusicVolume);
//     }
    
//     private void RestoreBackgroundMusic() {
//         // AudioManager.Instance.SetMusicVolume(originalMusicVolume);
//     }
    
//     // ============================================
//     // PUBLIC API - Dışarıdan çağrılabilir
//     // ============================================
//     public void ManualSave() {
//         SaveGameData();
//     }
    
//     public bool WasPlayingBeforePause() {
//         return wasPlayingBeforePause;
//     }
// }

