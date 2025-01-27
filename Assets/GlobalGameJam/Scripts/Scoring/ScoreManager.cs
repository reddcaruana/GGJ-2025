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
        /// Event binding for the add score event.
        /// </summary>
        private EventBinding<ScoreEvents.Add> onAddEventBinding;

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
        /// <param name="obj">The add score event data.</param>
        private void OnAddEventHandler(ScoreEvents.Add obj)
        {
            score += obj.Value;
            EventBus<ScoreEvents.Update>.Raise(new ScoreEvents.Update
            {
                Value = score
            });
        }

#endregion
    }
}