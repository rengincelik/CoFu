using UnityEngine;

[CreateAssetMenu(fileName = "JokerConfig", menuName = "Game/JokerConfig")]
public class JokerConfig : ScriptableObject
{
    public Sprite Icon;
    public Cost cost;
    public bool IsPassive;
    public JokerType Type;
}
