// UIScreen.cs
using UnityEngine;
using DG.Tweening;

namespace GameUI
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIScreen : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] protected AnimationType animationType = AnimationType.Fade;
        [SerializeField] protected float animationDuration = 0.3f;
        [SerializeField] protected Ease easeType = Ease.OutQuad;

        protected Canvas canvas;
        protected CanvasGroup canvasGroup;
        protected RectTransform rectTransform;

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            
            gameObject.SetActive(false);
        }

        public virtual void Show(bool animate = true)
        {
            gameObject.SetActive(true);
            
            if (animate)
            {
                PlayShowAnimation();
            }
            else
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            
            OnShow();
        }

        public virtual void Hide(bool animate = true)
        {
            canvasGroup.interactable = false;
            
            if (animate)
            {
                PlayHideAnimation(() => gameObject.SetActive(false));
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            }
            
            OnHide();
        }

        protected virtual void PlayShowAnimation()
        {
            canvasGroup.DOKill();
            rectTransform.DOKill();

            switch (animationType)
            {
                case AnimationType.Fade:
                    canvasGroup.alpha = 0f;
                    canvasGroup.DOFade(1f, animationDuration).SetEase(easeType).SetUpdate(true);
                    break;

                case AnimationType.Scale:
                    canvasGroup.alpha = 1f;
                    rectTransform.localScale = Vector3.zero;
                    rectTransform.DOScale(1f, animationDuration).SetEase(easeType).SetUpdate(true);
                    break;

                case AnimationType.SlideFromBottom:
                    canvasGroup.alpha = 1f;
                    Vector2 originalPos = rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector2(originalPos.x, -Screen.height);
                    rectTransform.DOAnchorPos(originalPos, animationDuration).SetEase(easeType).SetUpdate(true);
                    break;

                case AnimationType.SlideFromTop:
                    canvasGroup.alpha = 1f;
                    originalPos = rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector2(originalPos.x, Screen.height);
                    rectTransform.DOAnchorPos(originalPos, animationDuration).SetEase(easeType).SetUpdate(true);
                    break;
            }

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        protected virtual void PlayHideAnimation(System.Action onComplete = null)
        {
            canvasGroup.DOKill();
            rectTransform.DOKill();

            Sequence sequence = DOTween.Sequence();

            switch (animationType)
            {
                case AnimationType.Fade:
                    sequence.Append(canvasGroup.DOFade(0f, animationDuration).SetEase(easeType));
                    break;

                case AnimationType.Scale:
                    sequence.Append(rectTransform.DOScale(0f, animationDuration).SetEase(easeType));
                    break;

                case AnimationType.SlideFromBottom:
                    Vector2 targetPos = new Vector2(rectTransform.anchoredPosition.x, -Screen.height);
                    sequence.Append(rectTransform.DOAnchorPos(targetPos, animationDuration).SetEase(easeType));
                    break;

                case AnimationType.SlideFromTop:
                    targetPos = new Vector2(rectTransform.anchoredPosition.x, Screen.height);
                    sequence.Append(rectTransform.DOAnchorPos(targetPos, animationDuration).SetEase(easeType));
                    break;
            }

            sequence.SetUpdate(true);
            sequence.OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = false;
                onComplete?.Invoke();
            });
        }

        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        [ContextMenu("Test Show")]
        private void TestShow() => Show(true);

        [ContextMenu("Test Hide")]
        private void TestHide() => Hide(true);
    }

    public enum AnimationType
    {
        Fade,
        Scale,
        SlideFromBottom,
        SlideFromTop
    }
}
