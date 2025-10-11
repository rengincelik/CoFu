using System;
using UnityEngine;

public struct ColorVector
{
    public readonly int R, G, B;
    public bool IsNull;

    public static readonly ColorVector Null = new ColorVector { IsNull = true };

    public ColorVector(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
        IsNull = false;
    }


    // Modify your existing methods to handle null case
    public Color ToUnityColor()
    {
        return IsNull ? Color.clear : new Color(R, G, B);
    }

    // Update operator overloads to handle null cases
    public static ColorVector operator +(ColorVector a, ColorVector b)
    {
        if (a.IsNull || b.IsNull) return Null;
        return new ColorVector(a.R + b.R, a.G + b.G, a.B + b.B);
    }

    public static bool operator ==(ColorVector a, ColorVector b)
    {
        return a.R == b.R && a.G == b.G && a.B == b.B;
    }

    public static bool operator !=(ColorVector a, ColorVector b)
    {
        return !(a == b);
    }

    // Similarly update other operators and properties
    public bool IsWhite => !IsNull && R == 1 && G == 1 && B == 1;

    public bool IsValidColor => !IsNull &&
        (R == 0 || R == 1) && (G == 0 || G == 1) && (B == 0 || B == 1)
        && (R + G + B > 0); // Siyah değil
    public bool IsBaseColor =>
        (R + G + B == 1) && IsValidColor;
    public bool IsIntermediateColor =>
        (R + G + B == 2) && IsValidColor;


    public override string ToString() =>
        IsNull ? "Null" : $"({R}, {G}, {B})";

    public bool Equals(ColorVector other) => this == other;

    public override bool Equals(object obj) =>
        obj is ColorVector other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(R, G, B);
}
