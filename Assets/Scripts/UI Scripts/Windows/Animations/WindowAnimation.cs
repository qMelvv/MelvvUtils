using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MelvvUtils.UI
{
    /// <summary>
    /// Base abstract class to create ui window animations with DoTween plugin
    /// </summary>
    public abstract class WindowAnimation : MonoBehaviour
    {
        [Header("Base window animation settings")]
        [Space(5)]
        [SerializeField] protected float animationDuration = 1;
        [SerializeField] protected Ease easingMode;

        private UnityEvent _onAfterExit = null;
        private UnityEvent _onAfterEnter = null;

        public void SetupEvents(UnityEvent onAfterEnter, UnityEvent onAfterExit)
        {
            _onAfterExit = onAfterExit;
            _onAfterEnter = onAfterEnter;
        }

        public void Enter() => StartAnimation(true, _onAfterEnter);

        public void Exit() => StartAnimation(false, _onAfterExit);

        protected abstract void StartAnimation(bool isEntering, UnityEvent eventToInvoke);
    }
}
