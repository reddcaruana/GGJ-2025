using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Listens for score update events and updates the earnings text accordingly.
    /// </summary>
    public class EarningsListener : MonoBehaviour
    {
        /// <summary>
        /// The TextMeshPro text component to display the earnings.
        /// </summary>
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// Event binding for the ScoreEvents.Update event.
        /// </summary>
        private EventBinding<ScoreEvents.Update> onScoreUpdateEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the ScoreEvents.Update event.
        /// </summary>
        private void Awake()
        {
            onScoreUpdateEventBinding = new EventBinding<ScoreEvents.Update>(OnScoreUpdateEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the ScoreEvents.Update event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ScoreEvents.Update>.Register(onScoreUpdateEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the ScoreEvents.Update event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ScoreEvents.Update>.Deregister(onScoreUpdateEventBinding);
        }

        /// <summary>
        /// Resets the text field to the TMP_Text component found on this GameObject.
        /// </summary>
        private void Reset()
        {
            text = GetComponent<TMP_Text>();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the ScoreEvents.Update event.
        /// Updates the earnings text with the new score value.
        /// </summary>
        /// <param name="event">The ScoreEvents.Update event.</param>
        private void OnScoreUpdateEventHandler(ScoreEvents.Update @event)
        {
            text.text = $"Today's Earnings: <sprite index=\"0\"> {@event.Value}";
        }

#endregion
    }
}