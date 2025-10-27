
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelConfig[] levels;
    LevelStateData levelStateData;
    public LevelConfig GetLevelConfig(int level)
    {
        if (levels == null || levels.Length == 0)
            return null;

        return levels.FirstOrDefault(l => l.LevelNumber == level);
    }



}

