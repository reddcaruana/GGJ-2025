using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the level timer, handling timer updates and raising events.
    /// </summary>
    [RequireComponent(typeof(Timer))]
    public class LevelTimer : MonoBehaviour
    {
        /// <summary>
        /// The timer component used to track the level time.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Binding for the level start event.
        /// </summary>
        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the timer component.
        /// </summary>
        private void Awake()
        {
            timer = GetComponent<Timer>();

            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
        }

        /// <summary>
        /// Subscribes to the timer update event when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            timer.OnUpdate += OnTimerUpdateHandler;

            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
        }

        /// <summary>
        /// Unsubscribes from the timer update event when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            timer.OnUpdate -= OnTimerUpdateHandler;

            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Event handler for the level start event.
        /// Activates the timer when the level starts.
        /// </summary>
        /// <param name="obj">The level start event data.</param>
        private void OnLevelStartEventHandler(LevelEvents.Start obj)
        {
            timer.Activate();
        }

        /// <summary>
        /// Handles the timer update event, raising a level timer update event with the remaining time.
        /// </summary>
        /// <param name="current">The current time of the timer.</param>
        /// <param name="duration">The total duration of the timer.</param>
        private void OnTimerUpdateHandler(float current, float duration)
        {
            EventBus<LevelEvents.TimerUpdate>.Raise(new LevelEvents.TimerUpdate
            {
                Remaining = duration - current
            });
        }

#endregion
    }
}