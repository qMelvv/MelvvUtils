using System;
using System.Collections.Generic;
using UnityEngine;

namespace MelvvUtils.Utils
{
    /// <summary>
    /// Utility class for easy creation of timers
    /// </summary>
    public class FunctionTimer
    {
        private static List<FunctionTimer> _activeTimersList = new();
        private static MonobehaviorHook _monobehaviorHook;

        private event Action _onTimerEnd;
        private event Action<float> _onTimerUpdated;

        private float _time;
        private float _startTime;

        private bool _isRepeating;

        public bool IsWorking => _time > 0;


        private FunctionTimer(float time, Action onTimerEnd, bool repeating, Action<float> onTimerUpdated)
        {
            _onTimerEnd = onTimerEnd;
            _onTimerUpdated = onTimerUpdated;

            _time = time;
            _startTime = time;

            _isRepeating = repeating;
        }

        public static FunctionTimer Create(float time, Action onTimerEnd, bool repeating = false, Action<float> onTimerUpdated = null)
        {
            InitHook();

            FunctionTimer functionTimer = new FunctionTimer
                (time, onTimerEnd, repeating, onTimerUpdated);

            _activeTimersList.Add(functionTimer);
            _monobehaviorHook.OnUpdate += functionTimer.UpdateTimer;

            return functionTimer;
        }

        private static void RemoveTimer(FunctionTimer functionTimer)
        {
            InitHook();

            _activeTimersList.Remove(functionTimer);
            _monobehaviorHook.OnUpdate -= functionTimer.UpdateTimer;
        }

        private static void InitHook()
        {
            if (_monobehaviorHook != null) return;

            GameObject hook = new GameObject("Timer's Monobehavior hook", typeof(MonobehaviorHook));

            _monobehaviorHook = hook.GetComponent<MonobehaviorHook>();
        }

        private void UpdateTimer()
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                _onTimerEnd?.Invoke();

                if (_isRepeating)
                {
                    _time = _startTime;
                    return;
                }

                _time = 0;
                RemoveTimer(this);
            }

            _onTimerUpdated?.Invoke(_time);
        }

        public void DestroyTimer()
        {
            RemoveTimer(this);

            _time = 0;
            _onTimerUpdated?.Invoke(_time);
        }
    }
}
