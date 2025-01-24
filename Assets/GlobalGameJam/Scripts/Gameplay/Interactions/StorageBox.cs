using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class StorageBox : MonoBehaviour, IUsable
    {
        [SerializeField] private PickableObjectData pickableObjectData;
        
#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not null)
            {
                return;
            }

            playerContext.Bag.Carry(pickableObjectData);
        }

#endregion
    }
}