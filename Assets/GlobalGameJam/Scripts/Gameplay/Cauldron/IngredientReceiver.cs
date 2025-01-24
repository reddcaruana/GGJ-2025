using UnityEngine;

namespace GlobalGameJam.Gameplay.Cauldron
{
    public class IngredientReceiver : MonoBehaviour
    {
#region Lifecycle Events

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }
            
            ingredient.Despawn();
        }

#endregion
    }
}