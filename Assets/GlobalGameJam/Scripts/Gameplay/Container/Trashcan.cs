using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a trashcan that can be used by the player to discard items.
    /// </summary>
    public class Trashcan : MonoBehaviour, IUsable
    {
        /// <summary>
        /// Hash for the "Excited" animation parameter.
        /// </summary>
        private static readonly int AnimatorExcitedHash = Animator.StringToHash("Excited");

        /// <summary>
        /// Hash for the "Eat" animation trigger.
        /// </summary>
        private static readonly int AnimatorEatHash = Animator.StringToHash("Eat");

        /// <summary>
        /// The animator component for the trashcan.
        /// </summary>
        [SerializeField] private Animator animator;

        /// <summary>
        /// The object sprite renderer.
        /// </summary>
        [SerializeField] private SpriteRenderer trashSprite;

        /// <summary>
        /// Event binding for the TrashcanEvents.Excite event.
        /// </summary>
        private EventBinding<TrashcanEvents.Excite> onExciteEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the TrashcanEvents.Excite event.
        /// </summary>
        private void Awake()
        {
            onExciteEventBinding = new EventBinding<TrashcanEvents.Excite>(OnExciteEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the TrashcanEvents.Excite event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<TrashcanEvents.Excite>.Register(onExciteEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the TrashcanEvents.Excite event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<TrashcanEvents.Excite>.Deregister(onExciteEventBinding);
        }

#endregion

#region Implementation of IUsable

        /// <summary>
        /// Uses the trashcan with the given player context.
        /// </summary>
        /// <param name="playerContext">The context of the player using the trashcan.</param>
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.IsFull == false)
            {
                return;
            }

            trashSprite.sprite = playerContext.Bag.Contents.Sprite;
            
            animator.SetBool(AnimatorExcitedHash, false);
            animator.SetTrigger(AnimatorEatHash);

            playerContext.Bag.Clear();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the TrashcanEvents.Excite event.
        /// Sets the "Excited" animation parameter to true.
        /// </summary>
        /// <param name="event">The TrashcanEvents.Excite event.</param>
        private void OnExciteEventHandler(TrashcanEvents.Excite @event)
        {
            animator.SetBool(AnimatorExcitedHash, true);
        }

#endregion
    }
}