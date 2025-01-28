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
        /// The score initials.
        /// </summary>
        private readonly char[] initials = new char[4];

        /// <summary>
        /// Event binding for the add score event.
        /// </summary>
        private EventBinding<ScoreEvents.Add> onAddEventBinding;

        /// <summary>
        /// Event binding to set the player's initial.
        /// </summary>
        private EventBinding<ScoreEvents.SetInitial> onSetInitialEventBinding;

        /// <summary>
        /// Event binding to submit the team score.
        /// </summary>
        private EventBinding<ScoreEvents.Submit> onSubmitEventBinding;

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
            onSetInitialEventBinding = new EventBinding<ScoreEvents.SetInitial>(OnSetInitialEventHandler);
            onSubmitEventBinding = new EventBinding<ScoreEvents.Submit>(OnSubmitEventHandler);

            onExtendTimerEventBinding = new EventBinding<TimerEvents.Extend>(OnExtendTimerEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ScoreEvents.Add>.Register(onAddEventBinding);
            EventBus<ScoreEvents.SetInitial>.Register(onSetInitialEventBinding);
            EventBus<ScoreEvents.Submit>.Register(onSubmitEventBinding);

            EventBus<TimerEvents.Extend>.Register(onExtendTimerEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ScoreEvents.Add>.Deregister(onAddEventBinding);
            EventBus<ScoreEvents.SetInitial>.Deregister(onSetInitialEventBinding);
            EventBus<ScoreEvents.Submit>.Deregister(onSubmitEventBinding);

            EventBus<TimerEvents.Extend>.Deregister(onExtendTimerEventBinding);
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

        /// <summary>
        /// Handles the event where the player sets their initial.
        /// </summary>
        /// <param name="event">The set initial event data.</param>
        private void OnSetInitialEventHandler(ScoreEvents.SetInitial @event)
        {
            initials[@event.PlayerID] = @event.Initial;
        }

        /// <summary>
        /// Handles the event where the player submits their score.
        /// </summary>
        private void OnSubmitEventHandler(ScoreEvents.Submit @event)
        {
            var entry = new ScoreEntry
            {
                Name = new string(initials),
                Score = score,
                Time = time
            };

            EventBus<LeaderboardEvents.Add>.Raise(new LeaderboardEvents.Add
            {
                Entry = entry
            });
        }

#endregion
    }
}