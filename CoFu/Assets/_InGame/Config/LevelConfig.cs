using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ColorCombiner/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    //bu kısım level başlarken kullanılacak
    [SerializeField] GridConfig gridConfig;
    [SerializeField] JokerConfig[] jokerConfigs;
    // [SerializeField] LevelType levelType;
    [SerializeField] int levelNumber;


}
[Serializable]
public class LevelStateData
{
    //bu kısım level devam ederken işlenecek ve sonuç olacak
    public int score;
    public int coins;
    public float timeLeft;
    public int moveLeft;
    public int currentLevel;
    public bool isPaused;

}


