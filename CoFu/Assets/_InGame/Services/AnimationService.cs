using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationService : Singleton<AnimationService>
{
    [SerializeField] float duration = 1f;
    void Fade(GameObject obj, float to, float duration)
    {
        var image = obj.GetComponent<Image>();
        if (image != null)
        {
            Color c = image.color;
            image.DOColor(new Color(c.r, c.g, c.b, to), duration);
            return;
        }

        var text = obj.GetComponent<TMPro.TextMeshProUGUI>();
        if (text != null)
        {
            Color c = text.color;
            text.DOColor(new Color(c.r, c.g, c.b, to), duration);
            return;
        }

        Debug.LogWarning("Fade: No Image or TextMeshProUGUI found on object");
    }

    void Scale(GameObject obj, Vector3 to, float duration)
    {
        obj.transform.DOScale(to, duration);
    }


    void Slide(GameObject obj, Vector3 to, float duration)
    {
        obj.transform.DOLocalMove(to, duration);
    }

    public void FadeScale(GameObject obj, float alpha, Vector3 scale, float duration)
    {
        Sequence seq = DOTween.Sequence();
        seq.Join(obj.transform.DOScale(scale, duration));

        var image = obj.GetComponent<Image>();
        if (image != null)
            seq.Join(image.DOFade(alpha, duration));
        else
        {
            var text = obj.GetComponent<TMPro.TextMeshProUGUI>();
            if (text != null)
                seq.Join(text.DOFade(alpha, duration));
        }

        seq.Play();
    }

}
