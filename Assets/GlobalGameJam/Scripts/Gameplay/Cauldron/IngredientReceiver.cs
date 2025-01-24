using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class IngredientReceiver : MonoBehaviour
    {
        [SerializeField] private float duration = 3.0f;
        
        private CauldronManager cauldronManager;

        private IngredientRegistry ingredientRegistry;
        private IngredientQueue ingredientQueue;
        private IEnumerator<IngredientData> ingredientEnumerator;

        private IngredientData Expected => ingredientEnumerator.Current;
        
#region Lifecycle Events

        private void Awake()
        {
            cauldronManager = Singleton.GetOrCreateMonoBehaviour<CauldronManager>();
            
            ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            ingredientQueue = new IngredientQueue(ingredientRegistry.Ingredients);
            ingredientEnumerator = ingredientQueue.GetEnumerator();
        }

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }

            var data = ingredient.Data;
            ingredient.Despawn();
            
            if (Expected != data)
            {
                cauldronManager.Fail();
                return;
            }
            
            cauldronManager.Evaluate(ingredient.Data);
        }

#endregion
    }
}