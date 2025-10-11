using UnityEngine;

[CreateAssetMenu(fileName = "Joker", menuName = "ColorCombiner/JokerConfig")]
public class JokerConfig : ScriptableObject
{
    public JokerType type;
    public int whiteEssenceCost;
    public float cooldownDuration;
    public Sprite icon;

    [TextArea]
    public string description;

    // Özel parametreler
    public float timeFreezeDuration; // 10s
    public float hintGlowDuration; // 5s
}
