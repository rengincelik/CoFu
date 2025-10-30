using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum QuickAnimation
{
    SlideInLeft, SlideInRight, SlideInTop, SlideInBottom,
    SlideOutLeft, SlideOutRight, SlideOutTop, SlideOutBottom,
    Bounce, Punch, FadeIn, FadeOut, PopIn, PopOut
}

// [System.Serializable]
// public struct ItemAnimation
// {
//     public QuickAnimation animation;
//     public float duration;
// }

[System.Serializable]
public class AnimationSequenceItem
{
    
    public GameObject[] gameObjects;
    public QuickAnimation[] animations;
    public float duration;
    public float delay;
}


public static class TweenFactory
{
    private static readonly Dictionary<QuickAnimation, Func<GameObject, float, Tween>> _map =
        new()
        {
            // Fade Animations
            { QuickAnimation.FadeIn, (o, duration) => Fade(o, 1f, duration) },
            { QuickAnimation.FadeOut, (o, duration) => Fade(o, 0f, duration) },
            
            // Scale/Pop Animations
            { QuickAnimation.PopIn, (o, duration) => Scale(o, Vector3.zero, Vector3.one, duration) },
            { QuickAnimation.PopOut, (o, duration) => Scale(o, Vector3.one, Vector3.zero, duration) },
            
            // Slide In Animations (Uses SlideFromOffset)
            { QuickAnimation.SlideInLeft, (o, duration) => SlideFromOffset(o, Vector3.left * 1000, duration) },
            { QuickAnimation.SlideInRight, (o, duration) => SlideFromOffset(o, Vector3.right * 1000, duration) },
            { QuickAnimation.SlideInTop, (o, duration) => SlideFromOffset(o, Vector3.up * 1000, duration) },
            { QuickAnimation.SlideInBottom, (o, duration) => SlideFromOffset(o, Vector3.down * 1000, duration) },
            
            // Slide Out Animations (MISSING IMPLEMENTATION - now using SlideToOffset)
            { QuickAnimation.SlideOutLeft, (o, duration) => SlideToOffset(o, Vector3.left * 1000, duration) },
            { QuickAnimation.SlideOutRight, (o, duration) => SlideToOffset(o, Vector3.right * 1000, duration) },
            { QuickAnimation.SlideOutTop, (o, duration) => SlideToOffset(o, Vector3.up * 1000, duration) },
            { QuickAnimation.SlideOutBottom, (o, duration) => SlideToOffset(o, Vector3.down * 1000, duration) },
            
            // Effect Animations
            { QuickAnimation.Bounce, (o, duration) => Bounce(o, duration) }, // Updated signature
            { QuickAnimation.Punch, (o, duration) => Punch(o, 1f, duration, 1) },
        };

    public static Tween CreateTween(GameObject[] objs, QuickAnimation[] anims, float duration)
    {
        var sequence = DOTween.Sequence();

        foreach (var obj in objs)
        {
            foreach (var anim in anims)
            {
                if (_map.TryGetValue(anim, out var func))
                {
                    var tween = func(obj, duration);

                    if (tween != null)
                    {
                        // Set specific ease types
                        if (anim == QuickAnimation.Bounce)
                            tween.SetEase(Ease.OutBounce);
                        else if (anim == QuickAnimation.Punch)
                            tween.SetEase(Ease.Linear); // Punch kendi easing'ine sahip
                        else
                            tween.SetEase(Ease.Linear);

                        sequence.Join(tween); // Aynı anda oynat
                    }
                }
                else
                {
                    Debug.LogWarning($"[TweenFactory] Unknown animation: {anim}");
                }
            }
        }

        return sequence;
    }


    public static Tween Fade(GameObject obj, float to, float duration)
    {
        // CanvasGroup varsa (en temiz)
        if (obj.TryGetComponent<CanvasGroup>(out var canvasGroup))
            return canvasGroup.DOFade(to, duration);

        // Tüm Image'ları fade et
        var images = obj.GetComponentsInChildren<Image>();
        var texts = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

        if (images.Length == 0 && texts.Length == 0)
        {
            Debug.LogWarning($"[TweenFactory] Fade failed: No CanvasGroup/Image/Text on {obj.name}");
            return null;
        }

        var sequence = DOTween.Sequence();
        foreach (var img in images)
            sequence.Join(img.DOFade(to, duration));
        foreach (var txt in texts)
            sequence.Join(txt.DOFade(to, duration));

        return sequence;
    }

    public static Tween Scale(GameObject obj, Vector3 from, Vector3 to, float duration)
    {
        obj.transform.localScale = from;
        return obj.transform.DOScale(to, duration);
    }

    public static Tween Slide(GameObject obj, Vector3 from, Vector3 to, float duration)
    {
        obj.transform.localPosition = from;
        return obj.transform.DOLocalMove(to, duration);
    }

    private static Tween SlideFromOffset(GameObject obj, Vector3 offset, float duration)
    {
        // For 'Slide In', we want the animation to start from the offset position and move TO the current position.
        Vector3 originalPos = obj.transform.localPosition;

        // Save the original position so it can be restored if PrepareInitialState was not called
        // or if we use the tween directly.
        obj.transform.localPosition = originalPos + offset;

        return obj.transform.DOLocalMove(originalPos, duration);
    }

    private static Tween SlideToOffset(GameObject obj, Vector3 offset, float duration)
    {
        // For 'Slide Out', we want the animation to start from the current position and move TO the offset position.
        Vector3 targetPos = obj.transform.localPosition + offset;
        return obj.transform.DOLocalMove(targetPos, duration);
    }

    public static Tween Rotate(GameObject obj, Vector3 from, Vector3 to, float duration)
    {
        obj.transform.rotation = Quaternion.Euler(from);
        return obj.transform.DORotate(to, duration);
    }

    public static Tween Punch(GameObject obj, float strength, float duration, int vibrato)
    {
        // DOPunchScale works from the current scale and returns to it.
        return obj.transform.DOPunchScale(Vector3.one * strength, duration, vibrato);
    }

    // Bounce is typically a 'Punch' with OutBounce ease, or a Scale to 1 with OutBounce.
    public static Tween Bounce(GameObject obj, float duration)
    {
        // DOScale with OutBounce is a clean way to make it look like a "bounce in" effect.
        // It's assumed the object is already scaled to 0 or a small value if this is an "in" animation.
        // If it's a simple bounce effect, DOPunchScale is better. Given the name 'Bounce' is often for
        // a 'Pop In' effect, I'll keep the DOScale logic but simplify the arguments.
        // I will remove the setting of scale to 'from' to allow it to be used as a general 'bounce' effect.
        return obj.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBounce);
    }

    // --- Initial State Preparation ---
    public static void PrepareInitialState(GameObject[] objs, QuickAnimation[] anims)
    {
        foreach (var obj in objs)
        {
            foreach (var anim in anims)
            {
                switch (anim)
                {
                    case QuickAnimation.FadeIn:
                        SetAlpha(obj, 0f);
                        break;

                    case QuickAnimation.FadeOut:
                        SetAlpha(obj, 1f);
                        break;

                    case QuickAnimation.PopIn:
                    case QuickAnimation.Bounce: // Initial scale should be zero for a bounce-in effect
                        obj.transform.localScale = Vector3.zero;
                        break;

                    case QuickAnimation.PopOut:
                        obj.transform.localScale = Vector3.one;
                        break;

                    case QuickAnimation.SlideInLeft:
                        obj.transform.localPosition += Vector3.left * 1000f;
                        break;

                    case QuickAnimation.SlideInRight:
                        obj.transform.localPosition += Vector3.right * 1000f;
                        break;

                    case QuickAnimation.SlideInTop:
                        obj.transform.localPosition += Vector3.up * 1000f;
                        break;

                    case QuickAnimation.SlideInBottom:
                        obj.transform.localPosition += Vector3.down * 1000f;
                        break;

                    // SlideOut ve Punch için başlangıçta değişiklik yapmaya gerek yok
                    case QuickAnimation.SlideOutLeft:
                    case QuickAnimation.SlideOutRight:
                    case QuickAnimation.SlideOutTop:
                    case QuickAnimation.SlideOutBottom:
                    case QuickAnimation.Punch:
                        break;
                }
            }
        }
    }


    // public static void PrepareInitialState(GameObject[] obj, ItemAnimation[] anim)
    // {
    //     switch (anim.animation)
    //     {
    //         case QuickAnimation.FadeIn:
    //             SetAlpha(obj, 0f);
    //             break;

    //         case QuickAnimation.FadeOut:
    //             SetAlpha(obj, 1f);
    //             break;

    //         case QuickAnimation.PopIn:
    //         case QuickAnimation.Bounce: // Initial scale should be zero for a bounce-in effect
    //             obj.transform.localScale = Vector3.zero;
    //             break;

    //         case QuickAnimation.PopOut:
    //             obj.transform.localScale = Vector3.one;
    //             break;

    //         // Slide In - Set position to the offset
    //         case QuickAnimation.SlideInLeft:
    //             obj.transform.localPosition += Vector3.left * 1000f;
    //             break;

    //         case QuickAnimation.SlideInRight:
    //             obj.transform.localPosition += Vector3.right * 1000f;
    //             break;

    //         case QuickAnimation.SlideInTop:
    //             obj.transform.localPosition += Vector3.up * 1000f;
    //             break;

    //         case QuickAnimation.SlideInBottom:
    //             obj.transform.localPosition += Vector3.down * 1000f;
    //             break;

    //         // Slide Out - No change to initial state needed, it starts from its current position.
    //         case QuickAnimation.SlideOutLeft:
    //         case QuickAnimation.SlideOutRight:
    //         case QuickAnimation.SlideOutTop:
    //         case QuickAnimation.SlideOutBottom:
    //         case QuickAnimation.Punch:
    //             // Do nothing, object starts in its current state.
    //             break;
    //     }
    // }

    // --- Utility Methods ---

    private static void SetAlpha(GameObject obj, float alpha)
    {
        if (obj.TryGetComponent<CanvasGroup>(out var cg))
        {
            cg.alpha = alpha;
            return;
        }

        var images = obj.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            var c = img.color;
            img.color = new Color(c.r, c.g, c.b, alpha);
        }

        var texts = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        foreach (var txt in texts)
            txt.alpha = alpha;
    }

}

// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using DG.Tweening;

// public enum QuickAnimation
// {
//     SlideInLeft, SlideInRight, SlideInTop, SlideInBottom,
//     SlideOutLeft, SlideOutRight, SlideOutTop, SlideOutBottom,
//     Bounce, Punch, FadeIn, FadeOut, PopIn, PopOut
// }

// [System.Serializable]
// public struct ItemAnimation
// {
//     public QuickAnimation animation;
//     public float duration;
// }

// [System.Serializable]
// public class AnimationSequenceItem
// {
    
//     public GameObject[] gameObjects;
//     public ItemAnimation[] animations;
//     public float delay;
// }


// public static class TweenFactory
// {
//     private static readonly Dictionary<QuickAnimation, Func<GameObject, ItemAnimation, Tween>> _map =
//         new()
//         {
//             { QuickAnimation.FadeIn, (o, a) => Fade(o, 1f, a.duration) },
//             { QuickAnimation.FadeOut, (o, a) => Fade(o, 0f, a.duration) },
//             { QuickAnimation.PopIn, (o, a) => Scale(o, Vector3.zero, Vector3.one, a.duration) },
//             { QuickAnimation.PopOut, (o, a) => Scale(o, Vector3.one, Vector3.zero, a.duration) },
//             { QuickAnimation.SlideInLeft, (o, a) => SlideFromOffset(o, Vector3.left * 1000, a.duration) },
//             { QuickAnimation.SlideInRight, (o, a) => SlideFromOffset(o, Vector3.right * 1000, a.duration) },
//             { QuickAnimation.Bounce, (o, a) => Bounce(o,  Vector3.zero, Vector3.one, a.duration) },
//             { QuickAnimation.Punch, (o, a) => Punch(o, 1, a.duration, 1) },
//         };

//     public static Tween CreateTween(GameObject obj, ItemAnimation anim)
//     {
//         if (_map.TryGetValue(anim.animation, out var func))
//         {
//             var tween = func(obj, anim);
//             tween?.SetEase(Ease.Linear);
//             return tween;
//         }

//         Debug.LogWarning($"[TweenFactory] Unknown animation: {anim.animation}");
//         return null;
//     }



//     public static Tween Fade(GameObject obj, float to, float duration)
//     {
//         // CanvasGroup varsa (en temiz)
//         if (obj.TryGetComponent<CanvasGroup>(out var canvasGroup))
//             return canvasGroup.DOFade(to, duration);

//         // Tüm Image'ları fade et
//         var images = obj.GetComponentsInChildren<Image>();
//         var texts = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

//         if (images.Length == 0 && texts.Length == 0)
//         {
//             Debug.LogWarning($"[TweenFactory] Fade failed: No CanvasGroup/Image/Text on {obj.name}");
//             return null;
//         }

//         var sequence = DOTween.Sequence();
//         foreach (var img in images)
//             sequence.Join(img.DOFade(to, duration));
//         foreach (var txt in texts)
//             sequence.Join(txt.DOFade(to, duration));

//         return sequence;
//     }

//     public static Tween Scale(GameObject obj, Vector3 from, Vector3 to, float duration)
//     {
//         obj.transform.localScale = from;
//         return obj.transform.DOScale(to, duration);
//     }

//     public static Tween Slide(GameObject obj, Vector3 from, Vector3 to, float duration)
//     {
//         obj.transform.localPosition = from;
//         return obj.transform.DOLocalMove(to, duration);
//     }

//     private static Tween SlideFromOffset(GameObject obj, Vector3 offset, float duration)
//     {
//         Vector3 originalPos = obj.transform.localPosition;
//         obj.transform.localPosition = originalPos + offset;
//         return obj.transform.DOLocalMove(originalPos, duration);
//     }

//     private static Tween SlideToOffset(GameObject obj, Vector3 offset, float duration)
//     {
//         Vector3 originalPos = obj.transform.localPosition;
//         Vector3 targetPos = originalPos + offset;
//         return obj.transform.DOLocalMove(targetPos, duration);
//     }

//     public static Tween Rotate(GameObject obj, Vector3 from, Vector3 to, float duration)
//     {
//         obj.transform.rotation = Quaternion.Euler(from);
//         return obj.transform.DORotate(to, duration);
//     }

//     public static Tween Punch(GameObject obj, float strength, float duration, int vibrato)
//     {
//         return obj.transform.DOPunchScale(Vector3.one * strength, duration, vibrato);
//     }

//     public static Tween Bounce(GameObject obj, Vector3 from, Vector3 to, float duration)
//     {
//         obj.transform.localScale = from;
//         return obj.transform.DOScale(to, duration).SetEase(Ease.OutBounce);
//     }

//     public static void PrepareInitialState(GameObject obj, ItemAnimation anim)
//     {
//         switch (anim.animation)
//         {
//             case QuickAnimation.FadeIn:
//                 SetAlpha(obj, 0f);
//                 break;

//             case QuickAnimation.FadeOut:
//                 SetAlpha(obj, 1f);
//                 break;

//             case QuickAnimation.PopIn:
//                 obj.transform.localScale = Vector3.zero;
//                 break;

//             case QuickAnimation.PopOut:
//                 obj.transform.localScale = Vector3.one;
//                 break;

//             case QuickAnimation.SlideInLeft:
//                 obj.transform.localPosition += Vector3.left * 1000f;
//                 break;

//             case QuickAnimation.SlideInRight:
//                 obj.transform.localPosition += Vector3.right * 1000f;
//                 break;

//             case QuickAnimation.SlideInTop:
//                 obj.transform.localPosition += Vector3.up * 1000f;
//                 break;

//             case QuickAnimation.SlideInBottom:
//                 obj.transform.localPosition += Vector3.down * 1000f;
//                 break;

//         }
//     }


//     private static void SetAlpha(GameObject obj, float alpha)
//     {
//         if (obj.TryGetComponent<CanvasGroup>(out var cg))
//         {
//             cg.alpha = alpha;
//             return;
//         }

//         var images = obj.GetComponentsInChildren<Image>();
//         foreach (var img in images)
//         {
//             var c = img.color;
//             img.color = new Color(c.r, c.g, c.b, alpha);
//         }

//         var texts = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
//         foreach (var txt in texts)
//             txt.alpha = alpha;
//     }

// }


