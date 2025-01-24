using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class IngredientReceiver : MonoBehaviour
    {
        private CauldronManager cauldronManager; 
        
#region Lifecycle Events

        private void Awake()
        {
            cauldronManager = Singleton.GetOrCreateMonoBehaviour<CauldronManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }
            
            cauldronManager.Evaluate(ingredient.Data);
            ingredient.Despawn();
        }

#endregion
    }
}