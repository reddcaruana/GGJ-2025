using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class TimerValueListener : MonoBehaviour
    {
        [SerializeField] private FloatVariable timerValue;
        [SerializeField] private TMP_Text targetText;

#region Lifecycle Events

        private void OnEnable()
        {
            timerValue.OnValueChanged += OnTimerValueChanged;
        }

        private void OnDisable()
        {
            timerValue.OnValueChanged -= OnTimerValueChanged;
        }

#endregion

#region Subscriptions

        private void OnTimerValueChanged(float value)
        {
            targetText.text = TimeUtility.ToString(value);
        }

#endregion
    }
}