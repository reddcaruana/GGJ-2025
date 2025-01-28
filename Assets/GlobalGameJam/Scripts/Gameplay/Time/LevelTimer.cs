using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the level timer, handling timer updates and raising events.
    /// </summary>
    [RequireComponent(typeof(Timer))]
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private float timerDelay = 0.5f;

        /// <summary>
        /// The timer component used to track the level time.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Binding for the level start event.
        /// </summary>
        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;

        /// <summary>
        /// Binding for the change screen event.
        /// </summary>
        private EventBinding<LevelEvents.ChangeScreens> onChangeScreenEventBinding;
        
#region Lifecycle Events

        /// <summary>
        /// Initializes the timer component.
        /// </summary>
        private void Awake()
        {
            timer = GetComponent<Timer>();

            onChangeScreenEventBinding = new EventBinding<LevelEvents.ChangeScreens>(OnChangeScreenEventHandler);
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
        }

        /// <summary>
        /// Subscribes to the timer update event when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            timer.OnUpdate += OnTimerUpdateHandler;

            EventBus<LevelEvents.ChangeScreens>.Register(onChangeScreenEventBinding);
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
        }

        /// <summary>
        /// Unsubscribes from the timer update event when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            timer.OnUpdate -= OnTimerUpdateHandler;

            EventBus<LevelEvents.ChangeScreens>.Deregister(onChangeScreenEventBinding);
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Event handler for the change screen event.
        /// Deactivates the timer and raises a timer update event with the remaining time.
        /// </summary>
        /// <param name="event">The change screen event data.</param>
        private void OnChangeScreenEventHandler(LevelEvents.ChangeScreens @event)
        {
            timer.Deactivate();
            EventBus<LevelEvents.TimerUpdate>.Raise(new LevelEvents.TimerUpdate
            {
                Remaining = timer.Duration
            });
        }

        /// <summary>
        /// Event handler for the level start event.
        /// Activates the timer when the level starts.
        /// </summary>
        /// <param name="event">The level start event data.</param>
        private void OnLevelStartEventHandler(LevelEvents.Start @event)
        {
            StartCoroutine(ActivateTimerRoutine());
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

#region Coroutines

        /// <summary>
        /// Coroutine to activate the timer after a delay.
        /// </summary>
        /// <returns>IEnumerator for the coroutine.</returns>
        private IEnumerator ActivateTimerRoutine()
        {
            yield return new WaitForSeconds(timerDelay);
            Debug.Log("Timer!");
            timer.Activate();
        }

#endregion
    }
}