using System;
using UnityEngine;



[CreateAssetMenu(menuName = "Color System/Color Unit")]
public class ColorUnit : ScriptableObject
{
    [SerializeField] private Color displayColor; // sadece editor önizlemesi için
    [SerializeField] private Material material;   // UI'da göstermek için opsiyonel

    public Color DisplayColor => displayColor;
    public Material Material => material;
}
