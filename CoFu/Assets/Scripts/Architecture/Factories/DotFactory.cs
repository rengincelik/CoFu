using UnityEngine;

public class DotFactory : MonoBehaviour
{
    // [SerializeField] private ColorDotView dotPrefab;
    // [SerializeField] private Transform spawnParent;
    
    // private List<ColorDotView> activeDots = new List<ColorDotView>();
    
    // // Level başı spawn
    // public void SpawnInitialDots(List<ColorVector> colors, List<Vector2> positions)
    // {
    //     for (int i = 0; i < colors.Count; i++)
    //     {
    //         var dot = Instantiate(dotPrefab, positions[i], Quaternion.identity, spawnParent);
    //         dot.Initialize(colors[i]);
    //         activeDots.Add(dot);
    //     }
    // }
    
    // // Runtime'da renk değiştir (yeni spawn değil!)
    // public void ChangeDotColor(ColorDotView dot, ColorVector newColor)
    // {
    //     dot.UpdateColor(newColor); // Sadece material property block güncelle
    // }
    
    // // Level sonu temizlik
    // public void ClearAllDots()
    // {
    //     foreach (var dot in activeDots)
    //     {
    //         Destroy(dot.gameObject);
    //     }
    //     activeDots.Clear();
    // }
}
