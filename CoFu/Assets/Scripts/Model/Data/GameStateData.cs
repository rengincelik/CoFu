// GameStateData.cs
using System;

[Serializable]
public class GameStateData
{
    public int score;
    public int coins;
    public float timeLeft;
    public int currentLevel;
    public bool isPaused;
    public int jokersRemaining;

    // Save/Load için
    public string ToJson() => UnityEngine.JsonUtility.ToJson(this);
    public static GameStateData FromJson(string json) =>
        UnityEngine.JsonUtility.FromJson<GameStateData>(json);
}
