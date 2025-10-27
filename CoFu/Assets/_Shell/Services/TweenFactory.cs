using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TweenFactory
{
    public static Tween CreateTween(GameObject obj, ItemAnimation anim)
    {
        switch (anim.animation)
        {
            case QuickAnimation.FadeIn:
                return Fade(obj, 1f, anim.duration);

            case QuickAnimation.FadeOut:
                return Fade(obj, 0f, anim.duration);

            case QuickAnimation.PopIn:
                return Scale(obj, Vector3.zero, Vector3.one, anim.duration);

            case QuickAnimation.PopOut:
                return Scale(obj, Vector3.one, Vector3.zero, anim.duration);

            case QuickAnimation.SlideInLeft:
                return SlideFromOffset(obj, Vector3.left * 1000f, anim.duration);

            case QuickAnimation.SlideInRight:
                return SlideFromOffset(obj, Vector3.right * 1000f, anim.duration);

            case QuickAnimation.SlideInTop:
                return SlideFromOffset(obj, Vector3.up * 1000f, anim.duration);

            case QuickAnimation.SlideInBottom:
                return SlideFromOffset(obj, Vector3.down * 1000f, anim.duration);

            case QuickAnimation.SlideOutLeft:
                return SlideToOffset(obj, Vector3.left * 1000f, anim.duration);

            case QuickAnimation.SlideOutRight:
                return SlideToOffset(obj, Vector3.right * 1000f, anim.duration);

            // case QuickAnimation.Custom:
            //     return CreateCustomTween(obj, anim);

            default:
                return null;
        }
    }

    // private static Tween CreateCustomTween(GameObject obj, ItemAnimation anim)
    // {
    //     switch (anim.customType)
    //     {
    //         case AnimationType.Fade:
    //             return Fade(obj, anim.customAlpha, anim.duration);

    //         case AnimationType.Scale:
    //             return Scale(obj, anim.customFrom, anim.customTo, anim.duration);

    //         case AnimationType.Slide:
    //             return Slide(obj, anim.customFrom, anim.customTo, anim.duration);

    //         case AnimationType.Rotate:
    //             return Rotate(obj, anim.customFrom, anim.customTo, anim.duration);

    //         case AnimationType.Punch:
    //             return Punch(obj, anim.punchStrength, anim.duration, anim.punchVibrato);

    //         case AnimationType.Bounce:
    //             return Bounce(obj, anim.customFrom, anim.customTo, anim.duration);

    //         default:
    //             return null;
    //     }
    // }

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
        Vector3 originalPos = obj.transform.localPosition;
        obj.transform.localPosition = originalPos + offset;
        return obj.transform.DOLocalMove(originalPos, duration);
    }

    private static Tween SlideToOffset(GameObject obj, Vector3 offset, float duration)
    {
        Vector3 originalPos = obj.transform.localPosition;
        Vector3 targetPos = originalPos + offset;
        return obj.transform.DOLocalMove(targetPos, duration);
    }

    public static Tween Rotate(GameObject obj, Vector3 from, Vector3 to, float duration)
    {
        obj.transform.rotation = Quaternion.Euler(from);
        return obj.transform.DORotate(to, duration);
    }

    public static Tween Punch(GameObject obj, float strength, float duration, int vibrato)
    {
        return obj.transform.DOPunchScale(Vector3.one * strength, duration, vibrato);
    }

    public static Tween Bounce(GameObject obj, Vector3 from, Vector3 to, float duration)
    {
        obj.transform.localScale = from;
        return obj.transform.DOScale(to, duration).SetEase(Ease.OutBounce);
    }

    public static void PrepareInitialState(GameObject obj, ItemAnimation anim)
    {
        switch (anim.animation)
        {
            case QuickAnimation.FadeIn:
                SetAlpha(obj, 0f);
                break;

            case QuickAnimation.FadeOut:
                SetAlpha(obj, 1f);
                break;

            case QuickAnimation.PopIn:
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

            // case QuickAnimation.Custom:
            //     PrepareCustomState(obj, anim);
                // break;
        }
    }

    // private static void PrepareCustomState(GameObject obj, ItemAnimation anim)
    // {
    //     switch (anim.customType)
    //     {
    //         case AnimationType.Fade:
    //             SetAlpha(obj, 0f);
    //             break;

    //         case AnimationType.Scale:
    //         case AnimationType.Bounce:
    //             obj.transform.localScale = anim.customFrom;
    //             break;

    //         case AnimationType.Slide:
    //             obj.transform.localPosition = anim.customFrom;
    //             break;

    //         case AnimationType.Rotate:
    //             obj.transform.rotation = Quaternion.Euler(anim.customFrom);
    //             break;
    //     }
    // }

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

// using UnityEngine;
// using UnityEngine.UI;
// using DG.Tweening;

// public static class TweenFactory
// {
//     public static Tween CreateTween(GameObject obj, ItemAnimation anim)
//     {
//         switch (anim.animationType)
//         {
//             case AnimationType.Fade:
//                 return Fade(obj, anim.toAlpha, anim.duration);

//             case AnimationType.Scale:
//                 return Scale(obj, anim.from, anim.to, anim.duration);

//             case AnimationType.Slide:
//                 return Slide(obj, anim.from, anim.to, anim.duration);

//             case AnimationType.Rotate:
//                 return Rotate(obj, anim.from, anim.to, anim.duration);

//             case AnimationType.Punch:
//                 return Punch(obj, anim.punchStrength, anim.duration, anim.punchVibrato);

//             case AnimationType.Bounce:
//                 return Bounce(obj, anim.from, anim.to, anim.duration);

//             default:
//                 return null;
//         }
//     }

//     public static Tween Fade(GameObject obj, float to, float duration)
//     {
//         if (obj.TryGetComponent<Image>(out var image))
//             return image.DOFade(to, duration);

//         if (obj.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
//             return text.DOFade(to, duration);

//         Debug.LogWarning($"[AnimationService] Fade failed: No Image or TextMeshProUGUI found on {obj.name}");
//         return null;
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

//     public static Tween FadeScale(GameObject obj, float alpha, Vector3 scale, float duration)
//     {
//         var seq = DOTween.Sequence();
//         seq.Join(obj.transform.DOScale(scale, duration));

//         if (obj.TryGetComponent<Image>(out var image))
//             seq.Join(image.DOFade(alpha, duration));
//         else if (obj.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
//             seq.Join(text.DOFade(alpha, duration));

//         return seq;
//     }

//     public static void PrepareInitialState(GameObject obj, ItemAnimation anim)
//     {
//         switch (anim.animationType)
//         {
//             case AnimationType.Fade:
//                 // Alpha'yı 0'a çek
//                 if (obj.TryGetComponent<Image>(out var img))
//                     img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
//                 if (obj.TryGetComponent<TMPro.TextMeshProUGUI>(out var txt))
//                     txt.alpha = 0;
//                 break;

//             case AnimationType.Scale:
//             case AnimationType.Bounce:
//                 obj.transform.localScale = anim.from;
//                 break;

//             case AnimationType.Slide:
//                 obj.transform.localPosition = anim.from;
//                 break;

//             case AnimationType.Rotate:
//                 obj.transform.rotation = Quaternion.Euler(anim.from);
//                 break;

//             case AnimationType.Punch:
//                 // Punch için hazırlık gerekmez
//                 break;
//         }
//     }

// }


