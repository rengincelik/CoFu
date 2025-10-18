using UnityEngine;
public struct Cost
{
    public CurrencyType currencyType;
    public int Amount;
}

[CreateAssetMenu(fileName = "JokerConfig", menuName = "Game/JokerConfig")]
public class JokerConfig : ScriptableObject
{
    public Sprite Icon;
    public Cost cost;
    public bool IsPassive;
    public JokerType Type;
}
