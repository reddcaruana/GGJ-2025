using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class Timer : MonoBehaviour
    {
        [SerializeField] private FloatVariable duration;
        [SerializeField] private FloatVariable currentValue;

        private bool isRunning;

#region Lifecycle Events

        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            currentValue.Value -= Time.deltaTime;
            if (currentValue.Value >= duration.Value)
            {
                Deactivate();
            }
        }

#endregion

#region Methods

        [ContextMenu("Activate")]
        public void Activate()
        {
            currentValue.Value = duration.Value;
            isRunning = true;
        }

        public void Deactivate()
        {
            currentValue.Value = duration.Value;
            isRunning = false;
        }

        public void Extend(float value)
        {
            currentValue.Value += value;
        }

        public void Pause()
        {
            isRunning = false;
        }

#endregion
    }
}