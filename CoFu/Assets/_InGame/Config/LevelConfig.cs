using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public struct Level
{
    public Limit[] limit;
    public Target[] targets;
}
[System.Serializable]
public struct Target
{
    public CandyType CandyType;
    public int TargetCount;
}
public enum LimitType{Time, Move}
[System.Serializable]
public struct Limit
{
    public LimitType LimitType;
    public int LimitAmount;
    
}
[CreateAssetMenu(fileName = "Level", menuName = "InGame/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    //bu kısım level başlarken kullanılacak
    [SerializeField] GridConfig GridConfig;
    [SerializeField] public int LevelNumber;
    [SerializeField] Level Level;
    [SerializeField] CandyType[] AvailableCandyTypes;


}

[Serializable]
public class LevelStateData
{
    public int score;
    public int coins;
    public bool isPaused;

    // Limit takibi
    public Dictionary<LimitType, int> remainingLimits;
    // Örnek: {time: 45, move: 20}

    // Target takibi
    public Dictionary<CandyType, int> collectedCandies;
    // Örnek: {Red: 15, Blue: 8}

    // Helper methods
    public bool IsLimitReached()
    {
        foreach (var limit in remainingLimits.Values)
            if (limit <= 0) return true;
        return false;
    }

    public bool AreTargetsComplete(Target[] targets)
    {
        foreach (var target in targets)
        {
            int collected = collectedCandies.GetValueOrDefault(target.CandyType, 0);
            if (collected < target.TargetCount) return false;
        }
        return true;
    }
}


