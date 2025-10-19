using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Cost
{
    //tip için birşey gelecek
    public int Amount;
}

public interface IJoker { bool TryUseJoker(); }

[CreateAssetMenu(fileName = "JokerConfig", menuName = "Game/JokerConfig")]
public class JokerConfig : ScriptableObject
{
    public Cost cost;
    public bool IsPassive;
}

//her jokere koy

public class JokerHandler : MonoBehaviour
{
    public IJoker joker;
    public JokerConfig jokerConfig;
    public Image iconImage;
    public Button ad_Button;
    public Button use_Button;
    public TextMeshProUGUI costText;

    public void UpdateUI(int currentGoldAmount)
    {
        if (jokerConfig == null) return;

        bool canAfford = currentGoldAmount >= jokerConfig.cost.Amount;

        if (canAfford)
        {
            use_Button.gameObject.SetActive(true);
            ad_Button.gameObject.SetActive(false);
            costText.text = jokerConfig.cost.Amount.ToString();
        }
        else
        {
            use_Button.gameObject.SetActive(false);
            ad_Button.gameObject.SetActive(true);
            costText.text = jokerConfig.cost.Amount.ToString();
        }
    }


    // --- CONTEXT MENU TEST METOTLARI (Debug için) ---

    [ContextMenu("TEST: Parası VAR gibi güncelle")]
    public void Test_UpdateU_CanAfford()
    {
        if (jokerConfig != null)
        {
            Debug.Log("TEST: 'Parası Var' durumu güncellendi.");
            UpdateUI(jokerConfig.cost.Amount + 999); // Fiyattan daha fazla parası var
        }
        else
        {
            Debug.LogError("Lütfen JokerConfig ataması yapın.");
        }
    }

    [ContextMenu("TEST: Parası YOK gibi güncelle")]
    public void Test_UpdateU_CannotAfford()
    {
        if (jokerConfig != null)
        {
            Debug.Log("TEST: 'Parası Yok' durumu güncellendi.");
            UpdateUI(jokerConfig.cost.Amount - 1); // Fiyattan daha az parası var
        }
        else
        {
            Debug.LogError("Lütfen JokerConfig ataması yapın.");
        }
    }

    [ContextMenu("TEST: Gerçek EconomyService ile Güncelle (Oyun Çalışmalı)")]
    public void Test_UpdateUI_WithRealData()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Bu testi kullanmak için oyun 'Play' modunda olmalı.");
            return;
        }

        if (jokerConfig == null)
        {
            Debug.LogError("Lütfen JokerConfig ataması yapın.");
            return;
        }

        // EconomyService'e doğrudan (Singleton ile) ulaşıyoruz.
        // Bu, ideal mimaride istenmez ama debug testi için tam istediğiniz şeydir.
        EconomyService economy = EconomyService.Instance;
        if (economy == null)
        {
            Debug.LogError("EconomyService sahnede bulunamadı!");
            return;
        }

        // Config'deki CurrencyType'ı string'e çevirip servisten istiyoruz
        string currencyID = jokerConfig.ToString(); // Örn: "Gold"
        Currency currency = economy.GetCurrency(currencyID);

        if (currency != null)
        {
            Debug.Log($"TEST: Gerçek data ile güncelleniyor. Mevcut {currencyID}: {currency.Amount}");
            UpdateUI(currency.Amount);
        }
        else
        {
            Debug.LogError($"EconomyService'te '{currencyID}' adında bir para birimi bulunamadı!");
        }
    }
}
