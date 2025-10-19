using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color System/Color Fusion Table")]
public class ColorFusionTable : ScriptableObject
{
    [System.Serializable]
    public struct FusionEntry 
    {
        public ColorUnit a;
        public ColorUnit b;
        public ColorUnit result;
    }

    [Tooltip("Renk birleştirme kuralları listesi. Her birleştirme için sadece bir giriş yeterlidir (simetri ColorCombinations'da ele alınır).")]
    public List<FusionEntry> entries = new List<FusionEntry>();

}
