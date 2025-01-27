using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Handles the catching of ingredients when they enter the trigger collider.
    /// </summary>
    public class IngredientCatcher : MonoBehaviour
    {
        /// <summary>
        /// Called when another collider enters the trigger collider attached to this object.
        /// </summary>
        /// <param name="other">The collider that entered the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }

            var data = ingredient.Data;
            ingredient.Despawn();

            // Raise an event to notify that an ingredient has been added.
            EventBus<CauldronEvents.AddedIngredient>.Raise(new CauldronEvents.AddedIngredient
            {
                Ingredient = data
            });
        }
    }
}