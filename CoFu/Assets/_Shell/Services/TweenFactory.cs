using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;




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





// public static class TweenFactory
// {
//     public static Tween CreateTween(GameObject obj, ItemAnimation anim)
//     {
//         switch (anim.animation)
//         {
//             case QuickAnimation.FadeIn:
//                 return Fade(obj, 1f, anim.duration);

//             case QuickAnimation.FadeOut:
//                 return Fade(obj, 0f, anim.duration);

//             case QuickAnimation.PopIn:
//                 return Scale(obj, Vector3.zero, Vector3.one, anim.duration);

//             case QuickAnimation.PopOut:
//                 return Scale(obj, Vector3.one, Vector3.zero, anim.duration);

//             case QuickAnimation.SlideInLeft:
//                 return SlideFromOffset(obj, Vector3.left * 1000f, anim.duration);

//             case QuickAnimation.SlideInRight:
//                 return SlideFromOffset(obj, Vector3.right * 1000f, anim.duration);

//             case QuickAnimation.SlideInTop:
//                 return SlideFromOffset(obj, Vector3.up * 1000f, anim.duration);

//             case QuickAnimation.SlideInBottom:
//                 return SlideFromOffset(obj, Vector3.down * 1000f, anim.duration);

//             case QuickAnimation.SlideOutLeft:
//                 return SlideToOffset(obj, Vector3.left * 1000f, anim.duration);

//             case QuickAnimation.SlideOutRight:
//                 return SlideToOffset(obj, Vector3.right * 1000f, anim.duration);

//             // case QuickAnimation.Custom:
//             //     return CreateCustomTween(obj, anim);

//             default:
//                 return null;
//         }
//     }

//     // private static Tween CreateCustomTween(GameObject obj, ItemAnimation anim)
//     // {
//     //     switch (anim.customType)
//     //     {
//     //         case AnimationType.Fade:
//     //             return Fade(obj, anim.customAlpha, anim.duration);

//     //         case AnimationType.Scale:
//     //             return Scale(obj, anim.customFrom, anim.customTo, anim.duration);

//     //         case AnimationType.Slide:
//     //             return Slide(obj, anim.customFrom, anim.customTo, anim.duration);

//     //         case AnimationType.Rotate:
//     //             return Rotate(obj, anim.customFrom, anim.customTo, anim.duration);

//     //         case AnimationType.Punch:
//     //             return Punch(obj, anim.punchStrength, anim.duration, anim.punchVibrato);

//     //         case AnimationType.Bounce:
//     //             return Bounce(obj, anim.customFrom, anim.customTo, anim.duration);

//     //         default:
//     //             return null;
//     //     }
//     // }

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

//             // case QuickAnimation.Custom:
//             //     PrepareCustomState(obj, anim);
//                 // break;
//         }
//     }

//     // private static void PrepareCustomState(GameObject obj, ItemAnimation anim)
//     // {
//     //     switch (anim.customType)
//     //     {
//     //         case AnimationType.Fade:
//     //             SetAlpha(obj, 0f);
//     //             break;

//     //         case AnimationType.Scale:
//     //         case AnimationType.Bounce:
//     //             obj.transform.localScale = anim.customFrom;
//     //             break;

//     //         case AnimationType.Slide:
//     //             obj.transform.localPosition = anim.customFrom;
//     //             break;

//     //         case AnimationType.Rotate:
//     //             obj.transform.rotation = Quaternion.Euler(anim.customFrom);
//     //             break;
//     //     }
//     // }

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



