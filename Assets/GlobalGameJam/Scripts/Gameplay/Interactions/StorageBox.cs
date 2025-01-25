using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class StorageBox : MonoBehaviour, IUsable
    {
        [SerializeField] private CarryableData carryableData;
        private Carryable storedInstance;

#region Lifecycle Events

        private void Start()
        {
            switch (carryableData)
            {
                case IngredientData ingredientData:
                    var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
                    storedInstance = ingredientManager.Generate(ingredientData, transform);
                    break;
            }
            
            storedInstance.AttachedRigidbody.isKinematic = true;
        }

#endregion
        
#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not null)
            {
                return;
            }

            playerContext.Bag.Carry(carryableData);
        }

#endregion
    }
}