
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GridConfig gridConfig;
    [SerializeField] GameObject emtyTile;
    [SerializeField] GameObject playTile;

    float cellWidth, cellHeight;
    int gridWidth, gridHeight;
    [Range(0, 1)] public float verticalSpacing = 0.2f;
    [Range(0, 1)] public float horizontalSpacing = 0.2f;

    List<Vector2Int> GridPositions;
    List<GameObject> backGroundTiles = new List<GameObject>();

    Tile[,] grid;

    void Initialize()
    {
        gridWidth = gridConfig.gridWidth;
        gridHeight = gridConfig.gridHeight;
        SpriteRenderer sr = emtyTile.GetComponent<SpriteRenderer>();
        cellWidth = sr.bounds.size.x;
        cellHeight = sr.bounds.size.y;
        GridPositions = gridConfig.GetSpawnPositions();
        grid = new Tile[gridWidth, gridHeight];
    }


    void Awake()
    {
        Initialize();
    }
    public void ApplyGravity()
    {

    }
    public void SpawnNewTiles()
    {
        
    }


    [ContextMenu("Generate Back Grid")]
    public void GenerateBackGroundGrid()
    {
        Initialize();
        GenerateBackGrid();
    }
    [ContextMenu("Generate Play Grid")]
    public void GeneratePlayGroundGrid()
    {
        Initialize(); 
        GeneratePlayGrid();
    }
    public void GenerateBackGrid()
    {
        ClearGrid();
        
        Vector3 worldStartPoint=CalculateStartPosition();
        foreach (Vector2Int pos in GridPositions)
        {
            Vector3 worldPosition = CalculateWorldPosition(worldStartPoint, pos);
            GameObject obj = Instantiate(emtyTile, worldPosition, Quaternion.identity, transform);
            backGroundTiles.Add(obj);
        }

    }

    public void GeneratePlayGrid()
    {
        ClearGrid();
        
        Vector3 worldStartPoint=CalculateStartPosition();

        foreach (Vector2Int pos in GridPositions)
        {
            Vector3 worldPosition = CalculateWorldPosition(worldStartPoint, pos);
            GameObject obj = Instantiate(playTile, worldPosition, Quaternion.identity, transform);
            Tile tile = obj.GetComponent<Tile>();
            tile.Init(GetRandomType(), CandyState.basic);
            tile.worldPos = worldPosition;
            grid[pos.x, pos.y] = tile;
        }

    }
    public CandyType GetRandomType()
    {
        var values = System.Enum.GetValues(typeof(CandyType));
        int index = Random.Range(0, values.Length);
        return (CandyType)values.GetValue(index);
    }
    public Vector3 CalculateWorldPosition(Vector3 startPoint,Vector2Int pos)
    {

        return  new Vector3(
            startPoint.x + (cellWidth + horizontalSpacing) * pos.x + cellWidth / 2,
            startPoint.y + (cellHeight + verticalSpacing) * pos.y + cellHeight / 2,
            1
        );
        
    }
    public Vector3 CalculateStartPosition()
    {
        float totalWidth = gridWidth * cellWidth + horizontalSpacing * (gridWidth - 1);
        float totalHeight = gridHeight * cellHeight + verticalSpacing * (gridHeight - 1);
        return new Vector3(-totalWidth / 2, -totalHeight / 2, 1);

    }



    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        foreach (GameObject tile in backGroundTiles)
        {
            if (tile != null)
                DestroyImmediate(tile);
        }
        foreach (Tile tile in grid)
        {
            if (tile != null)
                DestroyImmediate(tile.gameObject);
        }
        
    }


    
}

