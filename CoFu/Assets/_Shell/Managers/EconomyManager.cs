using UnityEditor.Rendering;
using UnityEngine;

public interface ICurrency { }



[System.Serializable]
public class Currency
{
    public string CurrencyID;
    public int Amount;

}


public class EconomyManager:MonoBehaviour
{
    [SerializeField] CurrencyChangedEventSO currencyChangedEventSO;
    public Currency[] _currencies;

    public bool TrySpend(string id, int amount)
    {
        var currency = GetCurrency(id);
        if (currency == null) return false;
        
        if (currency.Amount < amount)
        {
            Debug.LogWarning($"Yetersiz {id}");
            return false;
        }

        currency.Amount -= amount;
        currencyChangedEventSO?.Raise();
        return true;
    }
    
    public void AddCurrency(string id, int amount)
    {
        if (amount <= 0) return;
        
        var currency = GetCurrency(id);
        if (currency == null) return;

        currency.Amount += amount;
        currencyChangedEventSO?.Raise();
    }
    
    public int GetAmount(string id)
    {
        var currency = GetCurrency(id);
        return currency?.Amount ?? 0;
    }
    
    public bool HasEnough(string id, int amount)
    {
        return GetAmount(id) >= amount;
    }

    // Private
    Currency GetCurrency(string id)
    {
        foreach (var currency in _currencies)
        {
            if (currency.CurrencyID == id)
                return currency;
        }
        return null;
    }
    void LoadAllCurrencies()
    {
        foreach (var currency in _currencies)
        {
            currency.Amount = SaveService.Load(currency.CurrencyID);
        }
    }
    
    void SaveAllCurrencies()
    {
        foreach (var currency in _currencies)
        {
            SaveService.Save(currency.CurrencyID,currency.Amount);
        }
    }




}

