using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay.Cauldron
{
    public class Concoction : MonoBehaviour
    {
        public event System.Action<IngredientData[]> OnIngredientsChanged;

        private readonly List<IngredientData> ingredients = new();

#region Lifecycle Events

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }
            
            ingredients.Add(ingredient.Data);
            ingredient.Despawn();
                
            OnIngredientsChanged?.Invoke(ingredients.ToArray());
        }

#endregion
    }
}