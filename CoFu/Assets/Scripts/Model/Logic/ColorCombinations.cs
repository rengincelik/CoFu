using UnityEngine;
using System.Collections.Generic;

public static class ColorCombinations
{
    private static ColorFusionTable tableAsset;
    private static ColorUnit whiteColorUnit;

    private static Dictionary<(ColorUnit, ColorUnit), ColorUnit> ruleMap;

    static ColorCombinations()
    {
        tableAsset = Resources.Load<ColorFusionTable>("ColorFusionTable");
        whiteColorUnit = Resources.Load<ColorUnit>("white");

        if (tableAsset != null)
        {
            BuildRuleMap(tableAsset);
        }
        else
        {
            Debug.LogError("ColorCombinations: ColorFusionTable yüklenemedi!");
        }
    }

    private static void BuildRuleMap(ColorFusionTable table)
    {
        ruleMap = new Dictionary<(ColorUnit, ColorUnit), ColorUnit>();

        foreach (var entry in table.entries)
        {
            if (entry.a == null || entry.b == null || entry.result == null)
                continue;
            
            ruleMap[(entry.a, entry.b)] = entry.result;
            ruleMap[(entry.b, entry.a)] = entry.result;
        }
    }

    public static bool TryFuse(ColorUnit a, ColorUnit b, out ColorUnit result)
    {
        result = null;

        if (a == null || b == null || ruleMap == null)
            return false;

        return ruleMap.TryGetValue((a, b), out result);
    }

    public static bool IsWhite(ColorUnit color)
    {
        return color == whiteColorUnit;
    }

}

