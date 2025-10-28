
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    public bool ApplyGravity()
    {
        bool anyTileMoved = false;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (IsSpawnPosition(x, y) && grid[x, y] == null)
                {
                    for (int checkY = y + 1; checkY < gridHeight; checkY++)
                    {
                        if (grid[x, checkY] != null)
                        {
                            Tile movingTile = grid[x, checkY];

                            grid[x, checkY] = null;
                            grid[x, y] = movingTile;

                            movingTile.gridPos = new Vector2Int(x, y);

                            Vector3 worldStartPoint = CalculateStartPosition();
                            Vector3 newWorldPos = CalculateWorldPosition(worldStartPoint, new Vector2Int(x, y));

                            movingTile.MoveToPosition(newWorldPos, 0.4f);

                            anyTileMoved = true;
                            break;
                        }
                    }
                }
            }
        }

        return anyTileMoved;
    }


    [ContextMenu("Generate Play Grid")]
    public void GeneratePlayGroundGrid()
    {
        Initialize();
        StartCoroutine(SpawnInitialGridWithDelay());
    }


    IEnumerator SpawnInitialGridWithDelay()
    {
        ClearPlayGrid();
        Vector3 worldStartPoint = CalculateStartPosition();

        Dictionary<int, List<Vector2Int>> columnGroups = new Dictionary<int, List<Vector2Int>>();

        foreach (Vector2Int pos in GridPositions)
        {
            if (!columnGroups.ContainsKey(pos.x))
                columnGroups[pos.x] = new List<Vector2Int>();

            columnGroups[pos.x].Add(pos);
        }

        for (int x = 0; x < gridWidth; x++)
        {
            if (columnGroups.ContainsKey(x))
            {
                foreach (Vector2Int pos in columnGroups[x])
                {
                    Vector3 finalWorldPosition = CalculateWorldPosition(worldStartPoint, pos);
                    float spawnHeightOffset = gridHeight * (cellHeight + verticalSpacing);
                    Vector3 spawnWorldPosition = finalWorldPosition + Vector3.up * spawnHeightOffset;

                    GameObject obj = Instantiate(playTile, spawnWorldPosition, Quaternion.identity, transform);
                    Tile tile = obj.GetComponent<Tile>();

                    tile.Init(GetRandomType(), CandyState.basic);
                    tile.gridPos = pos;

                    grid[pos.x, pos.y] = tile;
                    tile.MoveToPosition(finalWorldPosition, 0.5f);
                }

                yield return new WaitForSeconds(0.08f);
            }
        }

        yield return new WaitForSeconds(0.6f);
    }

    bool IsSpawnPosition(int x, int y)
    {
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
            return false;

        int index = y * gridWidth + x;
        return gridConfig.spawnPattern[index];
    }

    public void SpawnNewTiles()
    {
        Vector3 worldStartPoint = CalculateStartPosition();

        // Her kolon için
        for (int x = 0; x < gridWidth; x++)
        {
            // En üst sıradan başla ve yukarı doğru kontrol etmeye gerek yok (sadece en üste yeni tile koyacağız)
            for (int y = gridHeight - 1; y >= 0; y--)
            {
                // Bu pozisyon spawn pattern'da var mı VE boş mu?
                if (IsSpawnPosition(x, y) && grid[x, y] == null)
                {
                    // **Sadece en üstte boş olan pozisyonlara spawn etme mantığı:**
                    // Gravity uygulandıktan sonra, bir kolonun en üstündeki boşluklar için 
                    // yeni bir karo yaratılmalı. 
                    // Ancak tile'ı yaratırken, sanki bir üst satırdaymış gibi davranmalıyız.

                    Vector2Int spawnPos = new Vector2Int(x, y);

                    // 1. Tile'ın nihai hedef pozisyonu (yani boş olan grid[x, y] pozisyonu)
                    Vector3 finalWorldPosition = CalculateWorldPosition(worldStartPoint, spawnPos);

                    // 2. Tile'ın spawn edileceği yüksek pozisyonu hesapla.
                    // Yaratılan tile, grid'in hemen üstünden düşmelidir.
                    // x, y pozisyonunun bir üst sırası gibi düşünülmeli.

                    // İlk başta karoyu, grid'in hemen üstündeki boşluğun bir adım üstüne yerleştir.
                    float spawnYOffset = (cellHeight + verticalSpacing);
                    Vector3 spawnWorldPosition = finalWorldPosition + Vector3.up * spawnYOffset;

                    // Karoyu yarat
                    GameObject obj = Instantiate(playTile, spawnWorldPosition, Quaternion.identity, transform);
                    Tile tile = obj.GetComponent<Tile>();

                    // Tile bileşenini initialize et.
                    tile.Init(GetRandomType(), CandyState.basic);

                    // Tile'ın grid pozisyonunu ve hedef dünya pozisyonunu ayarla
                    tile.gridPos = spawnPos;

                    // Grid array'ine yerleştir
                    grid[x, y] = tile;

                    // Bu boşluk dolduğuna göre, bu kolonda daha aşağıda bir boşluk varsa, 
                    // o da bir sonraki ApplyGravity() çağrısında dolacaktır.
                    // Bu döngü, boşlukları yukarıdan aşağıya doğru doldurur.

                    // Not: Match-3 oyunlarında genellikle, sadece en üstteki boşluklar için değil, 
                    // bir kolonda kaç tane boşluk varsa, o kadar karo en üstten düşürülür. 
                    // Bu kod, mevcut boşluğa yeni bir karo atar ve bir sonraki ApplyGravity
                    // çağrısında o karonun aşağı inmesini bekler.
                }
            }
        }
    }



    [ContextMenu("Generate Back Grid")]
    public void GenerateBackGroundGrid()
    {
        Initialize();
        GenerateBackGrid();
    }


    public void GenerateBackGrid()
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


    public CandyType GetRandomType()
    {
        var values = System.Enum.GetValues(typeof(CandyType));
        int index = Random.Range(0, values.Length);
        return (CandyType)values.GetValue(index);
    }
    public Vector3 CalculateWorldPosition(Vector3 startPoint, Vector2Int pos)
    {

        return new Vector3(
            startPoint.x + (cellWidth + horizontalSpacing) * pos.x + cellWidth / 2,
            startPoint.y + (cellHeight + verticalSpacing) * pos.y + cellHeight / 2,
            0
        );

    }
    public Vector3 CalculateStartPosition()
    {
        float totalWidth = gridWidth * cellWidth + horizontalSpacing * (gridWidth - 1);
        float totalHeight = gridHeight * cellHeight + verticalSpacing * (gridHeight - 1);
        return new Vector3(-totalWidth / 2, -totalHeight / 2, 0);

    }



    [ContextMenu("Clear Play Grid")]
    public void ClearPlayGrid()
    {

        foreach (Tile tile in grid)
        {
            if (tile != null)
                DestroyImmediate(tile.gameObject);
        }

    }

    [ContextMenu("Clear Back Grid")]
    public void ClearBackGrid()
    {
        foreach (GameObject tile in backGroundTiles)
        {
            if (tile != null)
                DestroyImmediate(tile);
        }


    }

    [ContextMenu("Test Gravity")]
    void TestGravity()
    {
        bool moved = ApplyGravity();
        Debug.Log($"Gravity applied. Tiles moved: {moved}");
    }


    public bool isProcessing = false;

    public void RequestSwap(Tile tile, Vector2Int direction)
    {
        if (isProcessing) return;

        Tile neighbor = GetNeighborTile(tile, direction);

        if (neighbor == null)
        {
            // Geçersiz swap - komşu yok
            tile.SetState(CandyState.basic);
            Debug.Log("Invalid swap - no neighbor");
            return;
        }

        StartCoroutine(SwapSequence(tile, neighbor));
    }

    Tile GetNeighborTile(Tile tile, Vector2Int direction)
    {
        int newX = tile.gridPos.x + direction.x;
        int newY = tile.gridPos.y + direction.y;

        // Bounds check
        if (newX < 0 || newX >= gridWidth || newY < 0 || newY >= gridHeight)
            return null;

        return grid[newX, newY];
    }

    IEnumerator SwapSequence(Tile tile1, Tile tile2)
    {
        Debug.Log("=== SWAP STARTED ===");
        isProcessing = true;

        tile1.SetState(CandyState.basic);
        tile2.SetState(CandyState.basic);

        SwapTilesInGrid(tile1, tile2);

        yield return new WaitForSeconds(0.25f);

        bool hasMatch = CheckHorizontalMatch(tile1, out List<Tile> horizontalMatching1) ||
        CheckVerticalMatch(tile1, out List<Tile> verticalMatching1) ||
        CheckHorizontalMatch(tile2, out List<Tile> horizontalMatching2) ||
        CheckVerticalMatch(tile2, out List<Tile> verticalMatching2);

        if (!hasMatch)
        {
            Debug.Log("hasMatch");
            SwapTilesInGrid(tile1, tile2);
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
                // Tüm eşleşmeleri toplamak için bir liste
            List<Tile> allMatches = new List<Tile>();

            // 4 yön kontrolü
            if (CheckHorizontalMatch(tile1, out List<Tile> h1))
                allMatches.AddRange(h1);

            if (CheckVerticalMatch(tile1, out List<Tile> v1))
                allMatches.AddRange(v1);

            if (CheckHorizontalMatch(tile2, out List<Tile> h2))
                allMatches.AddRange(h2);

            if (CheckVerticalMatch(tile2, out List<Tile> v2))
                allMatches.AddRange(v2);

            // tekrar eden tile’ları çıkar (örneğin köşede kesişenler)
            allMatches = allMatches.Distinct().ToList();

            yield return StartCoroutine(HandleMatches(allMatches));
            ApplyGravity();
        }

        isProcessing = false;
        Debug.Log($"=== SWAP FINISHED === isProcessing: {isProcessing}");
    }
    IEnumerator HandleMatches(List<Tile> tiles)
    {
        foreach(var t in tiles)
        {
            grid[t.gridPos.x, t.gridPos.y] = null;
            t.DestroyTile();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void SwapTilesInGrid(Tile tile1, Tile tile2)
    {
        // Grid array'de swap
        Vector2Int pos1 = tile1.gridPos;
        Vector2Int pos2 = tile2.gridPos;

        grid[pos1.x, pos1.y] = tile2;
        grid[pos2.x, pos2.y] = tile1;

        // gridPos güncelle
        tile1.gridPos = pos2;
        tile2.gridPos = pos1;

        // worldPos hesapla ve hareket ettir
        Vector3 worldStartPoint = CalculateStartPosition();

        Vector3 newPos1 = CalculateWorldPosition(worldStartPoint, pos2);
        Vector3 newPos2 = CalculateWorldPosition(worldStartPoint, pos1);

        tile1.MoveToPosition(newPos1, 0.2f);
        tile2.MoveToPosition(newPos2, 0.2f);
    }


    bool CheckHorizontalMatch(Tile tile, out List<Tile> tiles)
    {
        tiles = new List<Tile>();
        int x = tile.gridPos.x;
        int y = tile.gridPos.y;
        CandyType type = tile.GetCandyType;

        // Başlangıç olarak mevcut tile'ı ekle
        tiles.Add(tile);

        // sola doğru kontrol
        int left = x - 1;
        while (left >= 0 && grid[left, y].GetCandyType == type)
        {
            tiles.Add(grid[left, y]);
            left--;
        }

        // sağa doğru kontrol
        int right = x + 1;
        while (right < gridWidth && grid[right, y].GetCandyType == type)
        {
            tiles.Add(grid[right, y]);
            right++;
        }

        return tiles.Count >= 3;
    }


    bool CheckVerticalMatch(Tile tile, out List<Tile> tiles)
    {
        tiles = new List<Tile>();
        int x = tile.gridPos.x;
        int y = tile.gridPos.y;
        CandyType type = tile.GetCandyType;
        tiles.Add(tile);
        int top = y + 1;
        while (top <= gridHeight && grid[x, top].GetCandyType == type)
        {
            tiles.Add(grid[x, top]);
            top++;
        }
        int down = y - 1;
        while (down >= 0 && grid[x, down].GetCandyType == type)
        {
            tiles.Add(grid[x, down]);
            down--;
        }
        return tiles.Count >= 3;
    }


}

