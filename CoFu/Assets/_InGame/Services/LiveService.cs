using UnityEngine;

public class Live
{
    public string liveKey = "Live";
    public int Amount = 5;

    public void ChangeAmount(int change) 
    { 
        Amount += change; 
        SaveAmount(); 
    }

    public void SaveAmount()
    {
        if (string.IsNullOrEmpty(liveKey))
        {
            Debug.LogError("Live anahtarı boş olamaz! Kayıt yapılamadı.");
            return;
        }
        // PlayerPrefs'e kaydetmeden önce Amount değerini kullanıyoruz.
        PlayerPrefs.SetInt(liveKey, Amount); 
        PlayerPrefs.Save();
    }

    public void LoadAmount()
    {
        if (string.IsNullOrEmpty(liveKey)) return;
        
        // Kayıtlı bir değer varsa onu yükler, yoksa mevcut Amount (varsayılan 5) değerini kullanır.
        Amount = PlayerPrefs.GetInt(liveKey, Amount);
    }
}

public class LiveService : Singleton<LiveService>
{
    public Live live;

    void Awake()
    {
        live = new Live();
        // 1. Önce kayıtlı değeri yüklemeliyiz.
        live.LoadAmount();

        // 2. Eğer ilk çalıştırma ise (kayıtlı değer yoksa), varsayılan değeri kaydetmek için (opsiyonel)
        // live.SaveAmount(); // Bu satır LoadAmount'tan hemen sonra genelde gereksizdir.
    }

    // Amount değerini doğrudan Live objesinden almalıyız, tekrar PlayerPrefs'i okumak yerine.
    public int GetLiveAmount()
    {
        // live objesinin güncel Amount değerini döndür.
        return live.Amount;
    }

    // --- INSPECTOR TEST BUTONLARI ---

    [ContextMenu("TEST: +1 Live Ekle")]
    public void Test_AddLive()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test butonlarını kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        live.ChangeAmount(+1);
        int currentAmount = live.Amount; // Güncel değeri live objesinden al.
        Debug.Log($"Can (Live) eklendi. Yeni Miktar: {currentAmount}");
    }

    // İsimlendirme hatası düzeltildi: Test_Remove1Live
    [ContextMenu("TEST: -1 Live Çıkar")]
    public void Test_RemoveLive() // Metot ismi Test_Remove10Gold yerine Test_RemoveLive olarak düzeltildi.
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test butonlarını kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        live.ChangeAmount(-1);
        int currentAmount = live.Amount; // Güncel değeri live objesinden al.
        Debug.Log($"Can (Live) çıkarıldı. Yeni Miktar: {currentAmount}");
    }
}

