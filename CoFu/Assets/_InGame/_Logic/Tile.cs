
using UnityEngine;

public enum CandyType{red,green,blue}
public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public CandyType candyType; // Enum: Red, Blue, Green, vb.

    public SpriteRenderer spriteRenderer;
    public bool isSelected = false;

    // Animation için
    public Vector3 worldPos;

    void Update()
    {
        // Smooth hareket
        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * 10f);
    }

}


