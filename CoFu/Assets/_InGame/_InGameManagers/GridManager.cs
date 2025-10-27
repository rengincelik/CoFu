
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GridConfig gridConfig;
    [SerializeField] GameObject emtyTile;

    float tileWidth, tileHeight;
    int gridWidth, gridHeight;
    [Range(0, 1)] public float verticalSpacing = 0.2f;
    [Range(0, 1)] public float horizontalSpacing = 0.2f;

    List<Vector2Int> GridPositions;
    List<GameObject> backGroundTiles = new List<GameObject>();

    void Initialize()
    {
        gridWidth = gridConfig.gridWidth;
        gridHeight = gridConfig.gridHeight;
        SpriteRenderer sr = emtyTile.GetComponent<SpriteRenderer>();
        tileWidth = sr.bounds.size.x;
        tileHeight = sr.bounds.size.y;
        GridPositions = gridConfig.GetSpawnPositions();
    }


    void Awake()
    {
        Initialize();
    }


    [ContextMenu("Generate Grid")]
    public void GenerateBackGroundGrid()
    {
        Initialize(); // Editor'da da çalışır
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        ClearGrid();
        Vector3 worldStartPoint;
        float totalWidth = gridWidth * tileWidth + horizontalSpacing * (gridWidth - 1);
        float totalHeight = gridHeight * tileHeight + verticalSpacing * (gridHeight - 1);
        worldStartPoint = new Vector3(-totalWidth / 2, -totalHeight / 2, 1);
        // for (int i = 0; i < gridWidth; i++)
        // {
        //     for (int j = 0; j < gridHeight; j++)
        //     {
        //         Vector3 worldPosition = new Vector3(worldStartPoint.x + (horizontalSpacing + tileWidth)*i,
        //             worldStartPoint.y + (verticalSpacing + tileHeight)*j,
        //             worldStartPoint.z);
        //         GameObject obj = Instantiate(emtyTile, worldPosition, Quaternion.identity, transform);
        //         backGroundTiles.Add(obj);

        //     }
        // }
        foreach (Vector2Int pos in GridPositions) // GridConfig'den gelen spawn positions
        {
            Vector3 worldPosition = new Vector3(
                worldStartPoint.x + (tileWidth + horizontalSpacing) * pos.x + tileWidth / 2,
                worldStartPoint.y + (tileHeight + verticalSpacing) * pos.y + tileHeight / 2,
                1
            );
            GameObject obj = Instantiate(emtyTile, worldPosition, Quaternion.identity, transform);
            backGroundTiles.Add(obj);
        }

    }



    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        foreach (GameObject tile in backGroundTiles)
        {
            if (tile != null)
                DestroyImmediate(tile); // Editor'da DestroyImmediate kullan
        }
        backGroundTiles.Clear();
    }




    
}

