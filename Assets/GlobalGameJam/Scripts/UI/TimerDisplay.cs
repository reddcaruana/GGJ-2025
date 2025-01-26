using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private Timer timer;
        
#region Methods

        public void Bind(Timer targetTimer)
        {
            timer = targetTimer;
            timer.OnUpdate += UpdateTimerHandler;
        }

#endregion

#region Event Handlers

        private void UpdateTimerHandler(float current, float duration)
        {
            var remaining = duration - current;
            var minutes = Mathf.Floor(remaining / 60);
            var seconds = (int)remaining % 60;

            timerText.text = $"{minutes:00}:{seconds:00}";
        }

#endregion
    }
}