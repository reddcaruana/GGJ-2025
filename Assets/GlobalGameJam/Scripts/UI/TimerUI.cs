using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Manages the UI for displaying the timer.
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        /// <summary>
        /// The text component used to display the timer.
        /// </summary>
        [SerializeField] private TMP_Text timerText;

        /// <summary>
        /// The event binding for handling level timer update events.
        /// </summary>
        private EventBinding<LevelEvents.TimerUpdate> onLevelTimerUpdateEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding.
        /// </summary>
        private void Awake()
        {
            onLevelTimerUpdateEventBinding = new EventBinding<LevelEvents.TimerUpdate>(OnLevelTimerUpdateEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.TimerUpdate>.Register(onLevelTimerUpdateEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.TimerUpdate>.Deregister(onLevelTimerUpdateEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the level timer update event, updating the timer text.
        /// </summary>
        /// <param name="event">The timer update event containing the remaining time.</param>
        private void OnLevelTimerUpdateEventHandler(LevelEvents.TimerUpdate @event)
        {
            var minutes = Mathf.FloorToInt(@event.Remaining / 60);
            var seconds = Mathf.FloorToInt(@event.Remaining % 60);

            if (@event.Remaining <= 0)
            {
                minutes = 0;
                seconds = 0;
            }

            timerText.text = $"{minutes:00}:{seconds:00}";
        }

#endregion
    }
}