using System;
using UnityEngine;

namespace MelvvUtils.Utils
{
    /// <summary>
    /// Hook to use Update in non-monobehaviours
    /// </summary>
    public class MonobehaviorHook : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
