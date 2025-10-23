using UnityEngine;

public enum AnimationType 
{ 
    None,
    Scale,
    Fade,
    Slide,
    Rotate,
    Punch,
    Bounce
}
[System.Serializable]
public struct ItemAnimation
{
    public AnimationType animationType;
    public float duration;
    public Vector3 from;
    public Vector3 to;
    public float toAlpha;
    public float punchStrength;
    public int punchVibrato;
}

[System.Serializable]
public class AnimationSequenceItem
{
    public GameObject gameObject;
    public ItemAnimation animation;
    public float delay;
}

