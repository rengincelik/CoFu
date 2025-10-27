
using UnityEngine;

public static class SaveService
{
    public static void Save(string id, int amount)
    {
        PlayerPrefs.SetInt(id, amount);
    }
    public static int Load(string id)
    {
        return PlayerPrefs.GetInt(id, 0);
    }
    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}