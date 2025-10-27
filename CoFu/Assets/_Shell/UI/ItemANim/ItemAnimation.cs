using UnityEngine;

public enum QuickAnimation
{
    FadeIn,
    FadeOut,
    PopIn,
    PopOut,
    SlideInLeft,
    SlideInRight,
    SlideInTop,
    SlideInBottom,
    SlideOutLeft,
    SlideOutRight,
    Custom
}

[System.Serializable]
public struct ItemAnimation
{
    public QuickAnimation animation;
    public float duration;
}


[System.Serializable]
public class AnimationSequenceItem
{
    public GameObject gameObject;
    public ItemAnimation animation;
    public float delay;
    // public AnimationSequenceItem[] nestedItems; 
}
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

// public enum AnimationPreset
// {
//     Custom,           // Manuel ayarlar
//     FadeIn,          // 0 → 1 alpha
//     FadeOut,         // 1 → 0 alpha
//     ScalePopIn,      // 0 → 1 scale
//     ScalePopOut,     // 1 → 0 scale
//     SlideFromLeft,   // Ekran dışı sol → center
//     SlideFromRight,  // Ekran dışı sağ → center
//     SlideFromTop,
//     SlideFromBottom,
//     SlideToLeft,     // Center → ekran dışı sol
//     SlideToRight,
//     SlideToTop,
//     SlideToBottom
// }

// [System.Serializable]
// public struct ItemAnimation
// {
//     public AnimationType animationType;
//     public AnimationPreset preset;

//     [Header("Timing")]
//     public float duration;
//     public float delay;

//     [Header("Custom Values (only if preset = Custom)")]
//     public Vector3 customFrom;
//     public Vector3 customTo;
//     public float customAlpha;

//     [Header("Special")]
//     public float punchStrength;
//     public int punchVibrato;
// }


// // [System.Serializable]
// // public struct ItemAnimation
// // {
// //     public AnimationType animationType;
// //     public float duration;
// //     public Vector3 from;
// //     public Vector3 to;
// //     public float toAlpha;
// //     public float punchStrength;
// //     public int punchVibrato;
// // }

