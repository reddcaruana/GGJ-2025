using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Manages the display of the score in the UI.
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        /// <summary>
        /// The text component used to display the score.
        /// </summary>
        [SerializeField] private TMP_Text scoreText;

        /// <summary>
        /// Event binding for the score update event.
        /// </summary>
        private EventBinding<ScoreEvents.Update> onScoreUpdateEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the score update event.
        /// </summary>
        private void Awake()
        {
            onScoreUpdateEventBinding = new EventBinding<ScoreEvents.Update>(OnScoreUpdateEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ScoreEvents.Update>.Register(onScoreUpdateEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ScoreEvents.Update>.Deregister(onScoreUpdateEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the score update event by updating the displayed score.
        /// </summary>
        /// <param name="event">The score update event data.</param>
        private void OnScoreUpdateEventHandler(ScoreEvents.Update @event)
        {
            scoreText.text = $"$ {@event.Earnings:N0}";
        }

#endregion
    }
}