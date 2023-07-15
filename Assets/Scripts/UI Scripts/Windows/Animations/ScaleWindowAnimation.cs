using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MelvvUtils.UI
{
    public class ScaleWindowAnimation : WindowAnimation
    {
        [Header("Scale animation settings")]
        [SerializeField] private Vector2 activeSize = new Vector3(1, 1, 1);
        [SerializeField] private Vector2 inactiveSize = Vector3.zero;

        private RectTransform _rectTransform;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        protected override void StartAnimation(bool isEntering, UnityEvent eventToInvoke)
        {
            Vector2 endSize = isEntering ? activeSize : inactiveSize;

            _rectTransform.DOScale(endSize, animationDuration)
                .SetEase(easingMode)
                .OnComplete(() => eventToInvoke?.Invoke())
                .SetLink(gameObject);
        }
    }
}
