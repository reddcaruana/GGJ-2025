using GlobalGameJam.Data;
using GlobalGameJam.Events;
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
        private EventBinding<TrashcanEvents.SetPootions> onExciteEventBinding;

        /// <summary>
        /// The number of trash items being held.
        /// </summary>
        private int availableTrash;

        /// <summary>
        /// The potion registry instance.
        /// </summary>
        private PotionRegistry potionRegistry;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the TrashcanEvents.Excite event.
        /// </summary>
        private void Awake()
        {
            potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            
            onExciteEventBinding = new EventBinding<TrashcanEvents.SetPootions>(OnExciteEventHandler);
        }

        /// <summary>
        /// Registers the TrashcanEvents.Excite event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<TrashcanEvents.SetPootions>.Register(onExciteEventBinding);
        }

        /// <summary>
        /// Deregisters the TrashcanEvents.Excite event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<TrashcanEvents.SetPootions>.Deregister(onExciteEventBinding);
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

            if (playerContext.Bag.Contents == potionRegistry.Pootion)
            {
                EventBus<TrashcanEvents.SetPootions>.Raise(new TrashcanEvents.SetPootions
                {
                    Count = -1
                });
            }
            
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
        private void OnExciteEventHandler(TrashcanEvents.SetPootions @event)
        {
            availableTrash = Mathf.Max(availableTrash + @event.Count, 0);
            animator.SetBool(AnimatorExcitedHash, availableTrash > 0);
        }

#endregion
    }
}