using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CandyDatabase", menuName = "Level/CandyDatabase")]
public class CandyDatabase : ScriptableObject
{
    [System.Serializable]
    public class CandyData
    {
        public CandyType type;
        public Sprite[] sprites;
        public int score = 10;
    }

    public List<CandyData> candies;

    public Sprite GetSprite(CandyType type, int frameIndex = 0)
    {
        CandyData data = candies.Find(c => c.type == type);
        if (data != null && data.sprites.Length > frameIndex)
            return data.sprites[frameIndex];
        return null;
    }

    public Sprite[] GetAnimationFrames(CandyType type)
    {
        CandyData data = candies.Find(c => c.type == type);
        return data?.sprites;
    }
    
}
