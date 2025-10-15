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

using System.Collections.Generic;
using UnityEngine;


public class JokerService : Singleton<JokerService>
{
    

}

