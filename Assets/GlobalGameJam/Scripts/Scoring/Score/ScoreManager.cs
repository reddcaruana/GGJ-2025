using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam
{
    /// <summary>
    /// Manages the score in the game, handling score updates and events.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// The current score.
        /// </summary>
        private int score;

        /// <summary>
        /// The time extensions applied.
        /// </summary>
        private float time;

        /// <summary>
        /// Event binding for the add score event.
        /// </summary>
        private EventBinding<ScoreEvents.Add> onAddEventBinding;

        /// <summary>
        /// Event binding for handling extensions in time.
        /// </summary>
        private EventBinding<TimerEvents.Extend> onExtendTimerEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the add score event.
        /// </summary>
        private void Awake()
        {
            onAddEventBinding = new EventBinding<ScoreEvents.Add>(OnAddEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ScoreEvents.Add>.Register(onAddEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ScoreEvents.Add>.Deregister(onAddEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the add score event by updating the score and raising a score update event.
        /// </summary>
        /// <param name="event">The add score event data.</param>
        private void OnAddEventHandler(ScoreEvents.Add @event)
        {
            score += @event.Value;
            EventBus<ScoreEvents.Update>.Raise(new ScoreEvents.Update
            {
                Value = score
            });
        }

        /// <summary>
        /// Handles the timer extension event by adding the duration to the current time.
        /// </summary>
        /// <param name="event">The timer extension event data.</param>
        private void OnExtendTimerEventHandler(TimerEvents.Extend @event)
        {
            time += @event.Duration;
        }

#endregion
    }
}