// Sorumluluklar:
// ├── Joker envanteri yönetimi
// ├── Joker kullanım kuralları (cooldown, limit)
// ├── Joker etkilerinin uygulanması
// └── Joker animasyon koordinasyonu

// Bağımlılıklar:
// ├── JokerConfig (ScriptableObject)
// ├── LevelService (süre ekleme için)
// └── DotFactory (shuffle için)

// Metodlar:
// ├── UseJoker(JokerType type)
// ├── GetJokerCount(JokerType type)
// ├── ApplyTimeFreeze(float duration)
// ├── ApplyShuffle()
// └── CanUseJoker(JokerType type)

// Joker Tipleri:
// ├── TimeFreeze (10 saniye dondur)
// ├── Shuffle (tüm dot'ları yeniden dağıt)
// └── HintGlow (doğru kombinasyonu göster)


using UnityEngine;

public class JokerService : MonoBehaviour
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
