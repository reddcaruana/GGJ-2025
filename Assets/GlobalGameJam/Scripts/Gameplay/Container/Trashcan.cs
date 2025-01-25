using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Trashcan : MonoBehaviour, IUsable
    {
#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            Debug.Log($"Trashing: {playerContext.Bag.IsFull}");
            
            if (playerContext.Bag.IsFull == false)
            {
                return;
            }

            playerContext.Bag.Clear();
        }

#endregion
    }
}