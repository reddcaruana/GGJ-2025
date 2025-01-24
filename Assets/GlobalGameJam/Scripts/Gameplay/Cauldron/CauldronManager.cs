using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay.Cauldron
{
    public class CauldronManager : MonoBehaviour
    {
        public event System.Action<IngredientData[]> OnIngredientsChanged;

        private PotionRegistry potionRegistry;
        private readonly List<IngredientData> ingredients = new();
        
#region Lifecycle Events

        private void Awake()
        {
            potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }
            
            ingredients.Add(ingredient.Data);
            ingredient.Despawn();

            Brew();
            OnIngredientsChanged?.Invoke(ingredients.ToArray());
        }

#endregion

#region Methods

        public void Brew()
        {
            if (ingredients.Count > potionRegistry.MaxIngredientCount)
            {
                // Empty the cauldron
                Debug.Log("Cauldron cannot handle this!");
                ingredients.Clear();
                return;
            }

            foreach (var potionData in potionRegistry.Potions)
            {
                if (potionData.IsComplete(ingredients.ToArray()))
                {
                    // Generate a potion
                    Debug.Log($"Potion generated! ({potionData.name})");
                    ingredients.Clear();
                    return;
                }
            }
        }

#endregion
    }
}