using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D.Animation;
[System.Serializable]
public class CandyData
{
    public CandyType type;
    public SpriteLibraryAsset spriteLibrary;
    public int score = 10;
}
    

[CreateAssetMenu( menuName = "CandyDatabase")]
public class CandyDatabase : ScriptableObject
{
    public List<CandyData> candies;
    public CandyData GetData(CandyType type)
        => candies.Find(c => c.type == type);
    public SpriteLibraryAsset GetLibrary(CandyType type)
    {
        var data = candies.Find(c => c.type == type);
        return data?.spriteLibrary;
    }




}
