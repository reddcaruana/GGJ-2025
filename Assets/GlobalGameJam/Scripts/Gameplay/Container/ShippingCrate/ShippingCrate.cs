using GlobalGameJam.Data;
using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a shipping bin that can be used by the player to sell potions.
    /// </summary>
    public class ShippingCrate : MonoBehaviour, IUsable
    {
        /// <summary>
        /// Registry of all potions in the game.
        /// </summary>
        private PotionRegistry potionRegistry;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the potion registry.
        /// </summary>
        private void Awake()
        {
            potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
        }

#endregion

#region Implementation of IUsable

        /// <summary>
        /// Uses the shipping bin with the given player context.
        /// </summary>
        /// <param name="playerContext">The context of the player using the shipping bin.</param>
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not PotionData potionData)
            {
                return;
            }

            playerContext.Bag.Clear();

            var potionCount = 1;
            var earnings = Mathf.Abs(potionData.Cost);
            var deductions = 0;

            if (potionData == potionRegistry.Pootion)
            {
                potionCount = 0;
                deductions = earnings;
                earnings = 0;
            }

            EventBus<ScoreEvents.Add>.Raise(new ScoreEvents.Add
            {
                Potions = potionCount,
                Deductions = deductions,
                Earnings = earnings
            });

            EventBus<TimerEvents.Extend>.Raise(new TimerEvents.Extend
            {
                Duration = potionData.Time
            });
            
            EventBus<ShippingCrateEvents.Add>.Raise(new ShippingCrateEvents.Add
            {
                Potion = potionData
            });
        }

#endregion
    }
}