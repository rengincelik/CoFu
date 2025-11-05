using UnityEngine;
public enum QuickAnimation
{
    SlideInLeft, SlideInRight, SlideInTop, SlideInBottom,
    SlideOutLeft, SlideOutRight, SlideOutTop, SlideOutBottom,
    Bounce, Punch, FadeIn, FadeOut, PopIn, PopOut
}

[System.Serializable]
public class AnimationSequenceItem
{
    
    public GameObject[] gameObjects;
    public QuickAnimation[] animations;
    public float duration;
    public float delay;
}




