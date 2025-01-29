using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Visual representation of a shipping crate that displays potions.
    /// </summary>
    public class ShippingCrateVisual : MonoBehaviour
    {
        /// <summary>
        /// Array of sprite renderers for displaying potion sprites.
        /// </summary>
        [SerializeField] private SpriteRenderer[] potionSprites;

        /// <summary>
        /// Index to keep track of the current potion sprite.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Event binding for the ShippingCrateEvents.Add event.
        /// </summary>
        private EventBinding<ShippingCrateEvents.Add> onAddPotionEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the potion sprites and event binding.
        /// </summary>
        private void Awake()
        {
            foreach (var potionSprite in potionSprites)
            {
                potionSprite.sprite = null;
            }

            onAddPotionEventBinding = new EventBinding<ShippingCrateEvents.Add>(OnAddPotionEventHandler);
        }

        /// <summary>
        /// Registers the ShippingCrateEvents.Add event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ShippingCrateEvents.Add>.Register(onAddPotionEventBinding);
        }

        /// <summary>
        /// Deregisters the ShippingCrateEvents.Add event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ShippingCrateEvents.Add>.Deregister(onAddPotionEventBinding);
        }

        /// <summary>
        /// Locates the sprite renderer components.
        /// </summary>
        private void Reset()
        {
            potionSprites = GetComponentsInChildren<SpriteRenderer>();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the ShippingCrateEvents.Add event.
        /// Updates the potion sprite based on the event data.
        /// </summary>
        /// <param name="event">The ShippingCrateEvents.Add event.</param>
        private void OnAddPotionEventHandler(ShippingCrateEvents.Add @event)
        {
            if (index >= potionSprites.Length)
            {
                return;
            }

            potionSprites[index].sprite = @event.Potion.Sprite;
            index++;
        }

#endregion
    }
}