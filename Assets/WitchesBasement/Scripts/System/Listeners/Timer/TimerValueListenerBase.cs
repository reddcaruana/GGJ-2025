using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.System
{
    internal abstract class TimerValueListenerBase : MonoBehaviour
    {
        [SerializeField] private FloatVariable timerValue;
        
#region Lifecycle Events

        protected virtual void OnEnable()
        {
            timerValue.OnValueChanged += OnTimerValueChanged;
        }

        protected virtual void OnDisable()
        {
            timerValue.OnValueChanged -= OnTimerValueChanged;
        }

#endregion

#region Subscriptions

        protected abstract void OnTimerValueChanged(float value);

#endregion
    }
}