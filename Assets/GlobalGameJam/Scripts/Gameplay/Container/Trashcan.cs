using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a trashcan that can be used by the player to discard items.
    /// </summary>
    public class Trashcan : MonoBehaviour, IUsable
    {
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

            playerContext.Bag.Clear();
        }

#endregion
    }
}