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
        /// Binding for the timer extension event.
        /// </summary>
        private EventBinding<TimerEvents.Extend> onExtendTimerEventBinding;

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

            onExtendTimerEventBinding = new EventBinding<TimerEvents.Extend>(OnExtendTimerEventHandler);
            onChangeScreenEventBinding = new EventBinding<LevelEvents.ChangeScreens>(OnChangeScreenEventHandler);
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
        }

        /// <summary>
        /// Subscribes to the timer update event when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            timer.OnUpdate += OnTimerUpdateHandler;
            timer.OnComplete += OnTimerCompleteHandler;
            
            EventBus<LevelEvents.ChangeScreens>.Register(onChangeScreenEventBinding);
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
            
            EventBus<TimerEvents.Extend>.Register(onExtendTimerEventBinding);
        }

        /// <summary>
        /// Unsubscribes from the timer update event when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            timer.OnUpdate -= OnTimerUpdateHandler;
            timer.OnComplete -= OnTimerCompleteHandler;

            EventBus<LevelEvents.ChangeScreens>.Deregister(onChangeScreenEventBinding);
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
            
            EventBus<TimerEvents.Extend>.Deregister(onExtendTimerEventBinding);
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
            EventBus<TimerEvents.Update>.Raise(new TimerEvents.Update
            {
                Remaining = timer.Duration
            });
        }

        /// <summary>
        /// Event handler for extending the timer.
        /// Extends the timer duration by the specified amount.
        /// </summary>
        /// <param name="event">The event data containing the duration to extend.</param>
        private void OnExtendTimerEventHandler(TimerEvents.Extend @event)
        {
            timer.Extend(@event.Duration);
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
        /// Event handler for when the timer completes.
        /// Raises the level end event.
        /// </summary>
        private void OnTimerCompleteHandler()
        {
            EventBus<LevelEvents.End>.Raise(LevelEvents.End.Default);
        }

        /// <summary>
        /// Handles the timer update event, raising a level timer update event with the remaining time.
        /// </summary>
        /// <param name="current">The current time of the timer.</param>
        /// <param name="duration">The total duration of the timer.</param>
        private void OnTimerUpdateHandler(float current, float duration)
        {
            EventBus<TimerEvents.Update>.Raise(new TimerEvents.Update
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
            timer.Activate();
        }

#endregion
    }
}