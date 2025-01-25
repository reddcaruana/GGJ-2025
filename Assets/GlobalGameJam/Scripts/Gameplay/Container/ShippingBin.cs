using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ShippingBin : MonoBehaviour, IUsable
    {
        public event System.Action<PotionData> OnUse; 
        
#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not PotionData potionData)
            {
                return;
            }
            
            OnUse?.Invoke(potionData);
            playerContext.Bag.Clear();
        }

#endregion
    }
}