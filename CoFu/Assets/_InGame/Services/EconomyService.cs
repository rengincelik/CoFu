using UnityEngine;

public interface ICurrency { }
[System.Serializable]
public class Currency
{
    public string currencyID;
    public int Amount;
    public void ChangeAmount(int change) { Amount += change; }
    public void SaveAmount()
    {
        if (string.IsNullOrEmpty(currencyID))
        {
            Debug.LogError("Currency ID boş olamaz! Kayıt yapılamadı.");
            return;
        }
        PlayerPrefs.SetInt(currencyID, Amount); PlayerPrefs.Save();
    }
    public void LoadAmount()
    {
        if (string.IsNullOrEmpty(currencyID)) return;
        
        Amount = PlayerPrefs.GetInt(currencyID, Amount);
    }
}


public class EconomyService : ServiceBase
{
    public Currency[] currencies;
    protected void Awake()
    {
        LoadAllCurrencies();
    }
    public Currency GetCurrency(string id)
    {
        foreach (var currency in currencies)
        {
            if (currency.currencyID == id)
            {
                return currency;
            }
        }
        Debug.LogWarning($"Para birimi bulunamadı: {id}");
        return null;
    }

    public void LoadAllCurrencies()
    {
        foreach (var currency in currencies)
        {
            currency.LoadAmount();
        }
    }


    // --- INSPECTOR TEST BUTONLARI ---

    // Inspector'da EconomyService bileşenine sağ tıklayıp "TEST: +100 Gold Ekle" seçeneğini seçin.
    // Metodun çalışması için oyunun "Play" modunda olması gerekir.
    [ContextMenu("TEST: +100 Gold Ekle")]
    public void Test_Add100Gold()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test butonlarını kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        Currency gold = GetCurrency("Gold"); // currencyID'si "Gold" olanı arar
        if (gold != null)
        {
            gold.ChangeAmount(100);
            Debug.Log($"TEST: 100 Gold eklendi. Yeni Miktar: {gold.Amount}");
            gold.SaveAmount(); // Test için hemen kaydedelim
        }
    }

    [ContextMenu("TEST: -10 Gold Çıkar")]
    public void Test_Remove10Gold()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test butonlarını kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        Currency gold = GetCurrency("Gold");
        if (gold != null)
        {
            gold.ChangeAmount(-10);
            Debug.Log($"TEST: 10 Gold çıkarıldı. Yeni Miktar: {gold.Amount}");
            gold.SaveAmount();
        }
    }

    [ContextMenu("TEST: +5 Gems Ekle")]
    public void Test_Add5Gems()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test butonlarını kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        Currency gems = GetCurrency("Gems"); // currencyID'si "Gems" olanı arar
        if (gems != null)
        {
            gems.ChangeAmount(5);
            Debug.Log($"TEST: 5 Gems eklendi. Yeni Miktar: {gems.Amount}");
            gems.SaveAmount();
        }
    }

    protected override void OnEventRaised()
    {
        Debug.Log("economy event dinlendi");
    }
}

