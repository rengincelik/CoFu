using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ColorCombiner/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] GridConfig gridConfig;
    [SerializeField] JokerConfig[] jokerConfigs;
    [SerializeField] LevelType levelType;
    [SerializeField] int levelNumber;


}

