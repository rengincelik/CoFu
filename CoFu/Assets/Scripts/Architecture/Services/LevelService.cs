// // Sorumluluklar:
// // ├── Aktif level config yönetimi
// // ├── Süre takibi (UniTask timer)
// // ├── Hedef kontrolleri (White Essence sayısı)
// // ├── Level bitirme/kaybetme logic
// // └── Dot spawn koordinasyonu

// // Bağımlılıklar:
// // ├── LevelConfig (ScriptableObject)
// // ├── DotFactory (spawn için)
// // └── SlotManager (doluluk kontrolü)

// // Metodlar:
// // ├── LoadLevel(int index)
// // ├── StartLevelTimer()
// // ├── CheckLevelComplete()
// // ├── SpawnInitialDots()
// // └── GetRemainingTime()

// // UniTask Kullanımı:
// // ├── async UniTask RunLevelTimer(CancellationToken ct)
// // └── await UniTask.Delay(1000, cancellationToken: ct)

// using UnityEngine;
// using System;

// // // public class LevelService : Singleton<LevelService> {
// // //     [Header("Current Level")]
// // //     public LevelConfig currentLevel;

// // //     // Runtime değişkenler
// // //     private float currentTime;
// // //     private int collectedWhiteEssence;
// // //     private int currentScore;

// // //     // Events
// // //     public event Action<float> OnTimeChanged;
// // //     public event Action<int> OnTargetProgress;
// // //     public event Action OnLevelWon;
// // //     public event Action OnLevelLost;

// // //     // Getters
// // //     public float RemainingTime => currentTime;
// // //     public int CollectedCount => collectedWhiteEssence;
// // //     public float Progress => CalculateProgress();

// // //     void Update() {
// // //         // Sadece Playing state'te çalış
// // //         if (GameService.Instance.CurrentState != GameState.Playing) {
// // //             return;
// // //         }

// // //         CheckWinCondition();
// // //         UpdateTimers();
// // //     }

// // //     public void LoadLevel(LevelConfig config) {
// // //         currentLevel = config;
// // //         ResetLevel();
// // //         StartLevel();
// // //     }

// // //     private void ResetLevel() {
// // //         currentTime = currentLevel.timeLimit;
// // //         collectedWhiteEssence = 0;
// // //         currentScore = 0;
// // //     }

// // //     private void StartLevel() {
// // //         // Dot spawn başlat
// // //         InvokeRepeating(nameof(SpawnDot), 0f, currentLevel.dotSpawnInterval);
// // //     }

// // //     private void UpdateTimers() {
// // //         // Sadece time-based modlarda timer çalışır
// // //         if (currentLevel.winCondition == WinConditionType.TimeBasedSurvival ||
// // //             currentLevel.winCondition == WinConditionType.Hybrid) {

// // //             currentTime -= Time.deltaTime;
// // //             OnTimeChanged?.Invoke(currentTime);

// // //             // Süre bitti - LOSE
// // //             if (currentTime <= 0f) {
// // //                 currentTime = 0f;

// // //                 // Hybrid modda hedef tamamlanmışsa WIN
// // //                 if (currentLevel.winCondition == WinConditionType.Hybrid &&
// // //                     collectedWhiteEssence >= currentLevel.targetWhiteEssence) {
// // //                     LevelWon();
// // //                 } else {
// // //                     LevelLost();
// // //                 }
// // //             }
// // //         }
// // //     }

// // //     private void CheckWinCondition() {
// // //         switch (currentLevel.winCondition) {
// // //             case WinConditionType.TimeBasedSurvival:
// // //                 // Süre dolunca Update'te kontrol ediliyor
// // //                 break;

// // //             case WinConditionType.CollectTarget:
// // //                 if (collectedWhiteEssence >= currentLevel.targetWhiteEssence) {
// // //                     LevelWon();
// // //                 }
// // //                 break;

// // //             case WinConditionType.ScoreBased:
// // //                 // TODO: Skor hedefi eklenirse
// // //                 break;

// // //             case WinConditionType.Hybrid:
// // //                 // Hem süre hem hedef - Update'te kontrol ediliyor
// // //                 break;
// // //         }
// // //     }

// // //     public void OnWhiteEssenceCollected() {
// // //         collectedWhiteEssence++;
// // //         OnTargetProgress?.Invoke(collectedWhiteEssence);

// // //         // Hedef tamamlandı mı kontrol et
// // //         CheckWinCondition();
// // //     }

// // //     private float CalculateProgress() {
// // //         switch (currentLevel.winCondition) {
// // //             case WinConditionType.TimeBasedSurvival:
// // //                 return 1f - (currentTime / currentLevel.timeLimit);

// // //             case WinConditionType.CollectTarget:
// // //                 return (float)collectedWhiteEssence / currentLevel.targetWhiteEssence;

// // //             case WinConditionType.Hybrid:
// // //                 // İki koşulun ortalaması
// // //                 float timeProgress = 1f - (currentTime / currentLevel.timeLimit);
// // //                 float targetProgress = (float)collectedWhiteEssence / currentLevel.targetWhiteEssence;
// // //                 return (timeProgress + targetProgress) / 2f;

// // //             default:
// // //                 return 0f;
// // //         }
// // //     }

// // //     private void LevelWon() {
// // //         CancelInvoke(nameof(SpawnDot));
// // //         GameService.Instance.EndGame(true);
// // //         OnLevelWon?.Invoke();
// // //     }

// // //     private void LevelLost() {
// // //         CancelInvoke(nameof(SpawnDot));
// // //         GameService.Instance.EndGame(false);
// // //         OnLevelLost?.Invoke();
// // //     }

// // //     private void SpawnDot() {
// // //         // DotFactory'den spawn edilecek
// // //         DotFactory.Instance.SpawnRandomDot();
// // //     }

// // //     public string GetObjectiveText()
// // //     {
// // //         switch (currentLevel.winCondition)
// // //         {
// // //             case WinConditionType.TimeBasedSurvival:
// // //                 return $"Survive for {currentLevel.timeLimit}s";

// // //             case WinConditionType.CollectTarget:
// // //                 return $"Collect {currentLevel.targetWhiteEssence} White Essence";

// // //             case WinConditionType.Hybrid:
// // //                 return $"Collect {currentLevel.targetWhiteEssence} in {currentLevel.timeLimit}s";

// // //             default:
// // //                 return "Complete the level";
// // //         }
// // //     }
// // //     // LevelService.cs
// // //     private void LevelWon()
// // //     {
// // //         int earnedEssence = collectedWhiteEssence;
// // //         int levelNumber = currentLevel.levelNumber;

// // //         // Para ekle
// // //         PlayerData.Instance.AddWhiteEssence(earnedEssence);

// // //         // High score kaydet
// // //         PlayerData.Instance.SaveHighScore(levelNumber, currentScore);

// // //         // Sonraki level'i aç
// // //         PlayerData.Instance.UnlockLevel(levelNumber + 1);

// // //         // Playtime kaydet
// // //         float sessionTime = currentLevel.timeLimit - currentTime;
// // //         PlayerData.Instance.AddPlaytime(sessionTime);

// // //         OnLevelWon?.Invoke();
// // //     }

// // // }

// // // ============================================
// // // 1. LEVEL SERVICE - Managed Version
// // // ============================================
// // public class LevelService : Singleton<LevelService>, ITickable {
// //     public LevelConfig currentLevel;
// //     private float currentTime;
// //     private int collectedWhiteEssence;
    
// //     // ITickable implementation
// //     public bool IsActive => GameService.Instance.CurrentState == GameState.Playing;
// //     public int Priority => 10; // İlk önce çalışır
    
// //     void OnEnable() {
// //         UpdateManager.Instance.RegisterGameplay(this);
// //     }
    
// //     void OnDisable() {
// //         UpdateManager.Instance.Unregister(this);
// //     }
    
// //     // ❌ ESKI: void Update() { ... }
// //     // ✅ YENİ:
// //     public void Tick(float deltaTime) {
// //         UpdateTimer(deltaTime);
// //         CheckWinCondition();
// //     }
    
// //     private void UpdateTimer(float deltaTime) {
// //         if (currentLevel.winCondition == WinConditionType.TimeBasedSurvival ||
// //             currentLevel.winCondition == WinConditionType.Hybrid) {
            
// //             currentTime -= deltaTime;
            
// //             if (currentTime <= 0f) {
// //                 currentTime = 0f;
// //                 OnTimeUp();
// //             }
// //         }
// //     }
    
// //     private void CheckWinCondition() {
// //         // Win condition kontrolleri...
// //     }
    
// //     private void OnTimeUp() {
// //         // Süre bitti logic...
// //     }
// // }

// // // ============================================
// // ÖRNEK 1: Level Service - MANAGED
// // (Timer sürekli çalışır, managed olmalı)
// // ============================================
// public class LevelService : Singleton<LevelService>, IUpdatable {
//     private float currentTime;
    
//     void OnEnable() {
//         // UpdateManager.Instance.Register(this);
//     }
    
//     void OnDisable() {
//         // UpdateManager.Instance.Unregister(this);
//     }
    
//     public void OnUpdate(float deltaTime) {
//         // Timer logic
//         if (currentTime > 0f) {
//             currentTime -= deltaTime;
//         }
//     }
// }

