using UnityEngine;
using UnityEngine.Events;

namespace MelvvUtils.UI
{
    /// <summary>
    /// Monobehavior class to create windows with code animations and enable/disable them
    /// </summary>
    [DisallowMultipleComponent]
    public class UIWindow : MonoBehaviour
    {
        [Header("Events")]
        [Space(10)]
        public UnityEvent OnBeforeEnter;
        public UnityEvent OnAfterEnter;
        public UnityEvent OnBeforeExit;
        public UnityEvent OnAfterExit;

        private GameObject _childVisuals;
        private WindowAnimation _windowAnimation;

        private void Awake()
        {
            _childVisuals = transform.GetChild(0).gameObject;
            _windowAnimation = GetComponent<WindowAnimation>();
        }

        private void Start()
        {
            _windowAnimation?.SetupEvents(OnAfterEnter, OnAfterExit);

            OnAfterExit.AddListener(() => _childVisuals.SetActive(false));

            ExitWindow();
        }

        public void EnterWindow()
        {
            _childVisuals.SetActive(true);
            OnBeforeEnter?.Invoke();

            if (_windowAnimation == null)
            {
                OnAfterEnter?.Invoke();
                return;
            }

            _windowAnimation.Enter();
        }

        public void ExitWindow()
        {
            OnBeforeExit?.Invoke();

            if (_windowAnimation == null)
            {
                OnAfterExit?.Invoke();
                return;
            }

            _windowAnimation.Exit();
        }
    }
}
