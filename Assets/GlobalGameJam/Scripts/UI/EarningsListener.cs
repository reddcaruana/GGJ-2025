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
        [SerializeField] private TMP_Text earningsText;
        
        /// <summary>
        /// The TextMeshPro text component to display the potion count.
        /// </summary>
        [SerializeField] private TMP_Text potionsText;
        
        /// <summary>
        /// The TextMeshPro text component to display the litter count.
        /// </summary>
        [SerializeField] private TMP_Text litterText;

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

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the ScoreEvents.Update event.
        /// Updates the earnings text with the new score value.
        /// </summary>
        /// <param name="event">The ScoreEvents.Update event.</param>
        private void OnScoreUpdateEventHandler(ScoreEvents.Update @event)
        {
            if (earningsText is not null)
            {
                earningsText.text = $"$ {@event.Earnings:N0}";
            }

            if (potionsText is not null)
            {
                potionsText.text = $"<sprite index=0> {@event.PotionCount}";
            }

            if (litterText is not null)
            {
                litterText.text = $"<sprite index=1> {@event.LitterCount}";
            }
        }

#endregion
    }
}