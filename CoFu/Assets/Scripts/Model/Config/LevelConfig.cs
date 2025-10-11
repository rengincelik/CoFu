using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ColorCombiner/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Grid Size")]
    public int gridWidth = 5;
    public int gridHeight = 5;
    
    [HideInInspector]
    public bool[] spawnPattern;
    
    public List<Vector2Int> GetSpawnPositions()
    {
        var positions = new List<Vector2Int>();
        
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int index = y * gridWidth + x;
                if (spawnPattern[index])
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }
        }
        
        return positions;
    }
    
    private void OnValidate()
    {
        int size = gridWidth * gridHeight;
        if (spawnPattern == null || spawnPattern.Length != size)
        {
            System.Array.Resize(ref spawnPattern, size);
        }
    }

}
