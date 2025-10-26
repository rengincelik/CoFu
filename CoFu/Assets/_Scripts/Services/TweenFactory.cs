using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TweenFactory
{
    public static Tween CreateTween(GameObject obj, ItemAnimation anim)
    {
        switch (anim.animationType)
        {
            case AnimationType.Fade:
                return Fade(obj, anim.toAlpha, anim.duration);

            case AnimationType.Scale:
                return Scale(obj, anim.from, anim.to, anim.duration);

            case AnimationType.Slide:
                return Slide(obj, anim.from, anim.to, anim.duration);

            case AnimationType.Rotate:
                return Rotate(obj, anim.from, anim.to, anim.duration);

            case AnimationType.Punch:
                return Punch(obj, anim.punchStrength, anim.duration, anim.punchVibrato);

            case AnimationType.Bounce:
                return Bounce(obj, anim.from, anim.to, anim.duration);

            default:
                return null;
        }
    }

    public static Tween Fade(GameObject obj, float to, float duration)
    {
        if (obj.TryGetComponent<Image>(out var image))
            return image.DOFade(to, duration);

        if (obj.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
            return text.DOFade(to, duration);

        Debug.LogWarning($"[AnimationService] Fade failed: No Image or TextMeshProUGUI found on {obj.name}");
        return null;
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

    public static Tween FadeScale(GameObject obj, float alpha, Vector3 scale, float duration)
    {
        var seq = DOTween.Sequence();
        seq.Join(obj.transform.DOScale(scale, duration));

        if (obj.TryGetComponent<Image>(out var image))
            seq.Join(image.DOFade(alpha, duration));
        else if (obj.TryGetComponent<TMPro.TextMeshProUGUI>(out var text))
            seq.Join(text.DOFade(alpha, duration));

        return seq;
    }
}
