using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a shipping bin that can be used by the player to sell potions.
    /// </summary>
    public class ShippingBin : MonoBehaviour, IUsable
    {
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

            EventBus<ScoreEvents.Add>.Raise(new ScoreEvents.Add
            {
                Value = potionData.Cost
            });

            EventBus<TimerEvents.Extend>.Raise(new TimerEvents.Extend
            {
                Duration = potionData.Time
            });
        }

#endregion
    }
}