using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "Level/GridConfig")]
public class GridConfig : ScriptableObject
{
    [Header("Grid Size")]
    public int gridWidth = 5;
    public int gridHeight = 5;

    [HideInInspector]
    public bool[] spawnPattern;

    // ... GetSpawnPositions method is fine ...
    public List<Vector2Int> GetSpawnPositions()
    {
        var positions = new List<Vector2Int>();

        // Add a safety check in case the array is null/wrong size in runtime
        if (spawnPattern == null || spawnPattern.Length != gridWidth * gridHeight)
            return positions;

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

    public void OnValidate()
    {
        // 1. Ensure minimum size is 1x1
        gridWidth = Mathf.Max(1, gridWidth);
        gridHeight = Mathf.Max(1, gridHeight);

        int requiredSize = gridWidth * gridHeight;

        // 2. Handle null or incorrect size
        if (spawnPattern == null)
        {
            spawnPattern = new bool[requiredSize];
        }
        else if (spawnPattern.Length != requiredSize)
        {
            // --- Data Preservation Logic (The Key Fix) ---

            // Create a temporary array of the new required size
            bool[] newPattern = new bool[requiredSize];

            // Determine the maximum length to copy (the smaller of the two sizes)
            int copyLength = Mathf.Min(spawnPattern.Length, requiredSize);

            // Copy existing data to the new array, preserving the top-left section
            System.Array.Copy(spawnPattern, newPattern, copyLength);

            // Replace the old array with the new, correctly sized and copied one
            spawnPattern = newPattern;
        }
    }
}
