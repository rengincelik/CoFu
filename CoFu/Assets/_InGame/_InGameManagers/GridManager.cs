
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GridManager : Singleton<GridManager>
{
    #region Serialized Fields
    [SerializeField] GridConfig gridConfig;
    [SerializeField] GameObject emtyTile;
    [SerializeField] GameObject playTile;

    [Range(0, 1)] public float verticalSpacing = 0.2f;
    [Range(0, 1)] public float horizontalSpacing = 0.2f;
    #endregion

    #region Private Fields
    float cellWidth, cellHeight;
    int gridWidth, gridHeight;
    List<Vector2Int> GridPositions;
    List<GameObject> backGroundTiles = new List<GameObject>();
    Tile[,] grid;
    public bool isProcessing = false;
    #endregion

    #region Unity Lifecycle
    void Awake()
    {
        Initialize();
    }
    #endregion

    #region Initialization & Grid Generation
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

    [ContextMenu("Generate Play Grid")]
    public void GeneratePlayGroundGrid()
    {
        Initialize();
        StartCoroutine(SpawnInitialGridWithDelay());
    }

    [ContextMenu("Generate Back Grid")]
    public void GenerateBackGroundGrid()
    {
        Initialize();
        GenerateBackGrid();
    }

    void GenerateBackGrid()
    {
        ClearBackGrid();
        Vector3 worldStartPoint = CalculateStartPosition();
        foreach (Vector2Int pos in GridPositions)
        {
            Vector3 worldPosition = CalculateWorldPosition(worldStartPoint, pos);
            GameObject obj = Instantiate(emtyTile, worldPosition, Quaternion.identity, transform);
            backGroundTiles.Add(obj);
        }
    }
    #endregion

    #region Initial Spawn
    IEnumerator SpawnInitialGridWithDelay()
    {
        ClearPlayGrid();
        Vector3 worldStartPoint = CalculateStartPosition();

        var columnGroups = GroupPositionsByColumn();

        for (int x = 0; x < gridWidth; x++)
        {
            if (columnGroups.ContainsKey(x))
            {
                foreach (Vector2Int pos in columnGroups[x])
                {
                    Vector3 finalPos = CalculateWorldPosition(worldStartPoint, pos);
                    float spawnHeight = gridHeight * (cellHeight + verticalSpacing);
                    Vector3 spawnPos = finalPos + Vector3.up * spawnHeight;

                    GameObject obj = Instantiate(playTile, spawnPos, Quaternion.identity, transform);
                    Tile tile = obj.GetComponent<Tile>();

                    tile.Init(GetRandomTypeWithoutMatch(pos.x, pos.y), CandyState.basic);
                    tile.gridPos = pos;
                    grid[pos.x, pos.y] = tile;

                    tile.MoveToPosition(finalPos, 0.5f);
                }
                yield return new WaitForSeconds(0.08f);
            }
        }
        yield return new WaitForSeconds(0.6f);
    }

    Dictionary<int, List<Vector2Int>> GroupPositionsByColumn()
    {
        var groups = new Dictionary<int, List<Vector2Int>>();
        foreach (Vector2Int pos in GridPositions)
        {
            if (!groups.ContainsKey(pos.x))
                groups[pos.x] = new List<Vector2Int>();
            groups[pos.x].Add(pos);
        }
        return groups;
    }
    #endregion

    #region Gravity & Spawning
    public bool ApplyGravity()
    {
        bool anyMoved = false;
        Vector3 startPoint = CalculateStartPosition();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (!IsSpawnPosition(x, y) || grid[x, y] != null) continue;

                for (int checkY = y + 1; checkY < gridHeight; checkY++)
                {
                    if (grid[x, checkY] != null)
                    {
                        Tile tile = grid[x, checkY];
                        grid[x, checkY] = null;
                        grid[x, y] = tile;
                        tile.gridPos = new Vector2Int(x, y);

                        Vector3 target = CalculateWorldPosition(startPoint, tile.gridPos);
                        tile.MoveToPosition(target, 0.4f);

                        anyMoved = true;
                        break;
                    }
                }
            }
        }
        return anyMoved;
    }
    // public void SpawnNewTiles()
    // {
    //     Vector3 startPoint = CalculateStartPosition();

    //     for (int x = 0; x < gridWidth; x++)
    //     {
    //         int topEmptyY = -1;

    //         for (int y = gridHeight - 1; y >= 0; y--)
    //         {
    //             if (IsSpawnPosition(x, y) && grid[x, y] == null)
    //             {
    //                 topEmptyY = y;
    //                 break;
    //             }
    //         }

    //         if (topEmptyY == -1) continue;

    //         Vector2Int spawnPos = new Vector2Int(x, topEmptyY);
    //         Vector3 finalPos = CalculateWorldPosition(startPoint, spawnPos);
    //         float spawnYOffset = (cellHeight + verticalSpacing);
    //         Vector3 spawnWorldPos = finalPos + Vector3.up * spawnYOffset;

    //         GameObject obj = Instantiate(playTile, spawnWorldPos, Quaternion.identity, transform);
    //         Tile tile = obj.GetComponent<Tile>();

    //         // ✅ Match kontrolü ile spawn
    //         tile.Init(GetRandomTypeWithoutMatch(x, topEmptyY), CandyState.basic);
    //         tile.gridPos = spawnPos;
    //         grid[x, topEmptyY] = tile;

    //         tile.MoveToPosition(finalPos, 0.4f);
    //     }
    // }

    public void SpawnNewTiles()
    {
        Vector3 startPoint = CalculateStartPosition();

        for (int x = 0; x < gridWidth; x++)
        {
            // ✅ Bu kolonda kaç boş hücre var say
            List<int> emptyYPositions = new List<int>();

            for (int y = 0; y < gridHeight; y++)
            {
                if (IsSpawnPosition(x, y) && grid[x, y] == null)
                {
                    emptyYPositions.Add(y);
                }
            }

            if (emptyYPositions.Count == 0) continue;

            // ✅ Her boş pozisyon için spawn et
            for (int i = 0; i < emptyYPositions.Count; i++)
            {
                int targetY = emptyYPositions[i];

                // Spawn pozisyonu: Grid üstünde, sırayla yüksekte
                float spawnHeight = (gridHeight + i + 1) * (cellHeight + verticalSpacing);
                Vector3 finalPos = CalculateWorldPosition(startPoint, new Vector2Int(x, targetY));
                Vector3 spawnPos = new Vector3(finalPos.x, startPoint.y + spawnHeight, finalPos.z);

                GameObject obj = Instantiate(playTile, spawnPos, Quaternion.identity, transform);
                Tile tile = obj.GetComponent<Tile>();

                tile.Init(GetRandomTypeWithoutMatch(x, targetY), CandyState.basic);
                tile.gridPos = new Vector2Int(x, targetY);
                grid[x, targetY] = tile;

                tile.MoveToPosition(finalPos, 0.4f);
            }
        }
    }

    #endregion

    #region Match Detection
    List<Tile> FindAllMatches()
    {
        var matches = new HashSet<Tile>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == null) continue;

                // Only check right & up to avoid duplicates
                TryAddHorizontalMatch(x, y, matches);
                TryAddVerticalMatch(x, y, matches);
            }
        }

        return matches.ToList();
    }

    void TryAddHorizontalMatch(int x, int y, HashSet<Tile> matches)
    {
        var line = new List<Tile>();
        CandyType type = grid[x, y].GetCandyType;

        for (int i = x; i < gridWidth; i++)
        {
            if (grid[i, y] != null && grid[i, y].GetCandyType == type)
                line.Add(grid[i, y]);
            else break;
        }

        if (line.Count >= 3)
            matches.UnionWith(line);
    }

    void TryAddVerticalMatch(int x, int y, HashSet<Tile> matches)
    {
        var line = new List<Tile>();
        CandyType type = grid[x, y].GetCandyType;

        for (int j = y; j < gridHeight; j++)
        {
            if (grid[x, j] != null && grid[x, j].GetCandyType == type)
                line.Add(grid[x, j]);
            else break;
        }

        if (line.Count >= 3)
            matches.UnionWith(line);
    }
    #endregion

    #region Cascade System
    // IEnumerator ProcessCascade()
    // {
    //     // 1. Match kontrolü
    //     List<Tile> matches = FindAllMatches();

    //     if (matches.Count > 0)
    //     {
    //         Debug.Log($"Cascade: {matches.Count} tiles matched");

    //         yield return StartCoroutine(HandleMatches(matches));
    //         ApplyGravity();
    //         yield return new WaitForSeconds(0.5f);

    //         SpawnNewTiles();
    //         yield return new WaitForSeconds(0.4f);

    //         ApplyGravity();
    //         yield return new WaitForSeconds(0.5f);

    //         // ✅ Tekrar cascade (match veya boşluk kontrolü)
    //         yield return StartCoroutine(ProcessCascade());
    //     }
    //     else
    //     {
    //         // ✅ Match yok ama boş hücre var mı?
    //         if (HasEmptyCells())
    //         {
    //             Debug.Log("No matches but empty cells - spawning");

    //             SpawnNewTiles();
    //             yield return new WaitForSeconds(0.4f);

    //             ApplyGravity();
    //             yield return new WaitForSeconds(0.5f);

    //             // Tekrar kontrol
    //             yield return StartCoroutine(ProcessCascade());
    //         }
    //         else
    //         {
    //             Debug.Log("Cascade complete - no matches and no empty cells");
    //         }
    //     }
    // }

    IEnumerator ProcessCascade()
    {
        List<Tile> matches = FindAllMatches();

        if (matches.Count > 0)
        {
            yield return StartCoroutine(HandleMatches(matches));

            ApplyGravity();
            yield return new WaitForSeconds(0.5f);

            SpawnNewTiles(); // ✅ Tüm boşlukları doldurur
            yield return new WaitForSeconds(0.5f);

            // ✅ Tekrar match kontrol
            yield return StartCoroutine(ProcessCascade());
        }
        else
        {
            Debug.Log("Cascade complete");
        }
    }

    bool HasEmptyCells()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (IsSpawnPosition(x, y) && grid[x, y] == null)
                {
                    return true; // Boş hücre var
                }
            }
        }
        return false; // Hepsi dolu
    }
    IEnumerator HandleMatches(List<Tile> tiles)
    {
        foreach (var tile in tiles)
        {
            grid[tile.gridPos.x, tile.gridPos.y] = null;
            tile.DestroyTile();
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion

    #region Swap Logic
    public void RequestSwap(Tile tile, Vector2Int direction)
    {
        if (isProcessing) return;

        Tile neighbor = GetNeighborTile(tile, direction);
        if (neighbor == null)
        {
            tile.SetState(CandyState.basic);
            return;
        }

        StartCoroutine(SwapSequence(tile, neighbor));
    }

    Tile GetNeighborTile(Tile tile, Vector2Int direction)
    {
        int nx = tile.gridPos.x + direction.x;
        int ny = tile.gridPos.y + direction.y;
        if (nx < 0 || nx >= gridWidth || ny < 0 || ny >= gridHeight) return null;
        return grid[nx, ny];
    }

    IEnumerator SwapSequence(Tile t1, Tile t2)
    {
        isProcessing = true;
        SwapTilesInGrid(t1, t2);
        yield return new WaitForSeconds(0.25f);

        bool hasMatch = HasMatchAt(t1.gridPos) || HasMatchAt(t2.gridPos);

        if (!hasMatch)
        {
            SwapTilesInGrid(t1, t2);
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return StartCoroutine(ProcessCascade());
        }

        isProcessing = false;
    }

    bool HasMatchAt(Vector2Int pos)
    {
        return CheckHorizontalMatch(grid[pos.x, pos.y], out _) ||
               CheckVerticalMatch(grid[pos.x, pos.y], out _);
    }

    void SwapTilesInGrid(Tile t1, Tile t2)
    {
        Vector2Int p1 = t1.gridPos, p2 = t2.gridPos;

        grid[p1.x, p1.y] = t2;
        grid[p2.x, p2.y] = t1;

        t1.gridPos = p2;
        t2.gridPos = p1;

        Vector3 start = CalculateStartPosition();
        t1.MoveToPosition(CalculateWorldPosition(start, p2), 0.2f);
        t2.MoveToPosition(CalculateWorldPosition(start, p1), 0.2f);
    }
    #endregion

    #region Match Check Helpers
    bool CheckHorizontalMatch(Tile tile, out List<Tile> tiles)
    {
        tiles = new List<Tile>();
        if (tile == null) return false;

        int x = tile.gridPos.x, y = tile.gridPos.y;
        CandyType type = tile.GetCandyType;
        tiles.Add(tile);

        for (int i = x - 1; i >= 0 && grid[i, y]?.GetCandyType == type; i--) tiles.Add(grid[i, y]);
        for (int i = x + 1; i < gridWidth && grid[i, y]?.GetCandyType == type; i++) tiles.Add(grid[i, y]);

        return tiles.Count >= 3;
    }

    bool CheckVerticalMatch(Tile tile, out List<Tile> tiles)
    {
        tiles = new List<Tile>();
        if (tile == null) return false;

        int x = tile.gridPos.x, y = tile.gridPos.y;
        CandyType type = tile.GetCandyType;
        tiles.Add(tile);

        for (int j = y - 1; j >= 0 && grid[x, j]?.GetCandyType == type; j--) tiles.Add(grid[x, j]);
        for (int j = y + 1; j < gridHeight && grid[x, j]?.GetCandyType == type; j++) tiles.Add(grid[x, j]);

        return tiles.Count >= 3;
    }
    #endregion

    #region Utility
    public CandyType GetRandomType()
    {
        var values = System.Enum.GetValues(typeof(CandyType));
        return (CandyType)values.GetValue(Random.Range(0, values.Length));
    }

    CandyType GetRandomTypeWithoutMatch(int x, int y)
    {
        int attempts = 0;
        while (attempts++ < 10)
        {
            CandyType type = GetRandomType();

            bool leftMatch = x >= 2 && grid[x - 1, y]?.GetCandyType == type && grid[x - 2, y]?.GetCandyType == type;
            bool bottomMatch = y >= 2 && grid[x, y - 1]?.GetCandyType == type && grid[x, y - 2]?.GetCandyType == type;

            if (!leftMatch && !bottomMatch) return type;
        }
        return GetRandomType();
    }

    bool IsSpawnPosition(int x, int y)
    {
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight) return false;
        int index = y * gridWidth + x;
        return gridConfig.spawnPattern[index];
    }

    public Vector3 CalculateWorldPosition(Vector3 start, Vector2Int pos)
    {
        return new Vector3(
            start.x + (cellWidth + horizontalSpacing) * pos.x + cellWidth / 2,
            start.y + (cellHeight + verticalSpacing) * pos.y + cellHeight / 2,
            0
        );
    }

    public Vector3 CalculateStartPosition()
    {
        float w = gridWidth * cellWidth + horizontalSpacing * (gridWidth - 1);
        float h = gridHeight * cellHeight + verticalSpacing * (gridHeight - 1);
        return new Vector3(-w / 2, -h / 2, 0);
    }
    #endregion

    #region Cleanup
    [ContextMenu("Clear Play Grid")]
    public void ClearPlayGrid()
    {
        if (grid == null) return;
        for (int x = 0; x < gridWidth; x++)
            for (int y = 0; y < gridHeight; y++)
                if (grid[x, y] != null)
                    DestroyImmediate(grid[x, y].gameObject);
        grid = new Tile[gridWidth, gridHeight];
    }

    [ContextMenu("Clear Back Grid")]
    public void ClearBackGrid()
    {
        foreach (var tile in backGroundTiles)
            if (tile != null) DestroyImmediate(tile);
        backGroundTiles.Clear();
    }

    [ContextMenu("Test Gravity")]
    void TestGravity()
    {
        bool moved = ApplyGravity();
        Debug.Log($"Gravity: {moved}");
    }
    #endregion
}




