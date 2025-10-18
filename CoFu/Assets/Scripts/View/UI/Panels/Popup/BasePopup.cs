// BasePopup.cs
using UnityEngine;
using DG.Tweening;

public abstract class BasePopup : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected RectTransform content;

    public virtual void Show()
    {
        gameObject.SetActive(true);

        // Fade in + scale animation
        canvasGroup.alpha = 0;
        content.localScale = Vector3.zero;

        canvasGroup.DOFade(1, 0.3f);
        content.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        OnShown();
    }

    public virtual void Hide()
    {
        canvasGroup.DOFade(0, 0.2f);
        content.DOScale(0, 0.2f).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));

        OnHidden();
    }

    protected virtual void OnShown() { }
    protected virtual void OnHidden() { }
}
