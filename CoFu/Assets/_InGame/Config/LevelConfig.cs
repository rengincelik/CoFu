using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public struct Level
{
    //bunlar 0 ise bence limit yok gibi çalışsın sistem
    public int timeLimit;
    public int targetWhite;
}
[CreateAssetMenu(fileName = "Level", menuName = "ColorCombiner/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    //bu kısım level başlarken kullanılacak
    [SerializeField] GridConfig gridConfig;
    [SerializeField] int levelNumber;
    [SerializeField] Level level;


}
[Serializable]
public class LevelStateData
{
    //bu kısım level devam ederken işlenecek ve sonuç olacak
    public int score;
    public int coins;
    public float timeLeft;
    public int targetRemaining;
    public int currentLevel;
    public bool isPaused;

}


