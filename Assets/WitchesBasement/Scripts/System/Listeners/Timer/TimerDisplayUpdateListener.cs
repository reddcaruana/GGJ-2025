using TMPro;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class TimerDisplayUpdateListener : TimerValueListenerBase
    {
        [SerializeField] private TMP_Text targetText;
        
#region Overrides of TimerValueListenerBase

        protected override void OnTimerValueChanged(float value)
        {
            targetText.text = TimeUtility.ToString(value);
        }

#endregion
    }
}