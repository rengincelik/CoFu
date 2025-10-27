using System;
using UnityEngine;

[System.Serializable]
public class Life
{
    public string LifeKey;
    public int Amount;
    public const int MaxLife = 5;
    public const long RegenerationPeriod = 1200; // saniye
}

// ----------------------
// Life Service (veri yönetimi)
// ----------------------
public static class LifeSaveService
{
    public static void SaveLife(Life life)
    {
        SaveService.Save(life.LifeKey, life.Amount);
    }

    public static int LoadLife(Life life)
    {
        return SaveService.Load(life.LifeKey);
    }

    public static void SaveLastLifeTime(Life life, long timeBinary)
    {
        PlayerPrefs.SetString(life.LifeKey + "_LastUpdate", timeBinary.ToString());
        PlayerPrefs.Save();
    }

    public static long LoadLastLifeTime(Life life)
    {
        string str = PlayerPrefs.GetString(life.LifeKey + "_LastUpdate", "0");
        long.TryParse(str, out long binary);
        return binary;
    }
}

