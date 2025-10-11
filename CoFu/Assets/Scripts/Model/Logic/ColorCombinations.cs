using System.Collections.Generic;
using UnityEngine;


public static class ColorCombinations
{
    // Renk matematik tablosu (statik lookup)
    private static Dictionary<(ColorType, ColorType), ColorType> rules = new() 
    {
        { (ColorType.Red, ColorType.Green), ColorType.Yellow },
        { (ColorType.Green, ColorType.Blue), ColorType.Cyan },
        { (ColorType.Red, ColorType.Blue), ColorType.Magenta },

        // White Essence kombinasyonları
        { (ColorType.Yellow, ColorType.Blue), ColorType.White },
        { (ColorType.Cyan, ColorType.Red), ColorType.White },
        { (ColorType.Magenta, ColorType.Green), ColorType.White }
    };

    public static ColorType? TryCombine(ColorType slot, ColorType dot)
    {
        if (rules.TryGetValue((slot, dot), out var result))
            return result;

        // Sıra önemli değil
        if (rules.TryGetValue((dot, slot), out result))
            return result;

        return null; // Invalid combination
    }

    public static bool IsWhiteEssence(ColorType color)
        => color == ColorType.White;
}

public static class ColorFusion
{
    public static bool CanFuse(ColorVector a, ColorVector b)
    {
        if (a.IsNull || b.IsNull) return false;
        if (a == b) return false;

        var merged = a + b;
        return merged.IsValidColor;
    }

    public static ColorVector Fuse(ColorVector a, ColorVector b)
    {
        if (CanFuse(a, b))
        {
            return new ColorVector(
                Mathf.Clamp(a.R + b.R, 0, 1),
                Mathf.Clamp(a.G + b.G, 0, 1),
                Mathf.Clamp(a.B + b.B, 0, 1)
            );
        }
        return new ColorVector(0, 0, 0); // ya da ColorVector.Invalid


    }
}
