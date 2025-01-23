using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class IngredientBox : MonoBehaviour, IUsable
    {
        [SerializeField] private IngredientData ingredient;
        
#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not null)
            {
                return;
            }

            playerContext.Bag.Carry(ingredient);
        }

#endregion
    }
}