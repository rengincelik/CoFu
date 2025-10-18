// Sorumluluklar:
// ├── White Essence (ana para birimi) yönetimi
// ├── Skorlama sistemi
// ├── Joker satın alma kontrolü
// └── Oyuncu envanter güncelleme

// Bağımlılıklar:
// ├── PlayerData
// └── JokerConfig (fiyat bilgisi)

// Metodlar:
// ├── AddWhiteEssence(int amount)
// ├── SpendWhiteEssence(int amount)
// ├── AddScore(int points)
// ├── CanAffordJoker(JokerType type)
// └── BuyJoker(JokerType type)
using UnityEngine;

public struct Cost
{
    public CurrencyType currencyType;
    public int Amount;
}

public class EconomyService : Singleton<EconomyService>
{
    public int WhiteEssence { get; private set; }

    public event System.Action<int> OnEssenceChanged;

    public void AddEssence(int amount)
    {
        WhiteEssence += amount;
        OnEssenceChanged?.Invoke(WhiteEssence);
    }
}

