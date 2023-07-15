using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MelvvUtils.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeWindowAnimation : WindowAnimation
    {
        [Header("Fade animation settings")]
        [SerializeField] private float activeAlpha = 1;
        [SerializeField] private float inactiveAlpha = 0;

        private CanvasGroup _canvasGroup;

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

        protected override void StartAnimation(bool isEntering, UnityEvent eventToInvoke)
        {
            float endAlphaValue = isEntering ? activeAlpha : inactiveAlpha;

            _canvasGroup.DOFade(endAlphaValue, animationDuration)
                .SetEase(easingMode)
                .OnComplete(() => eventToInvoke?.Invoke())
                .SetLink(gameObject);
        }
    }
}
