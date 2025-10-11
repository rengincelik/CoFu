// Sorumluluklar:
// ├── Aktif level config yönetimi
// ├── Süre takibi (UniTask timer)
// ├── Hedef kontrolleri (White Essence sayısı)
// ├── Level bitirme/kaybetme logic
// └── Dot spawn koordinasyonu

// Bağımlılıklar:
// ├── LevelConfig (ScriptableObject)
// ├── DotFactory (spawn için)
// └── SlotManager (doluluk kontrolü)

// Metodlar:
// ├── LoadLevel(int index)
// ├── StartLevelTimer()
// ├── CheckLevelComplete()
// ├── SpawnInitialDots()
// └── GetRemainingTime()

// UniTask Kullanımı:
// ├── async UniTask RunLevelTimer(CancellationToken ct)
// └── await UniTask.Delay(1000, cancellationToken: ct)


using UnityEngine;


public class LevelService : Singleton<LevelService>
{
    // // PaintManager'ın logic'ini al
    // public async UniTask SpawnDotsSequentially(
    //     List<ColorVector> colors,
    //     CancellationToken ct)
    // {
    //     foreach (var color in colors)
    //     {
    //         var dot = DotFactory.Instance.SpawnDot(GetRandomPosition(), color);
    //         await UniTask.Delay(300, cancellationToken: ct);
    //     }
    // }
}
