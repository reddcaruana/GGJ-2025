using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    internal class IngredientEvaluationListener : MonoBehaviour
    {
        [SerializeField] private IngredientDataVariable targetIngredient;
        [SerializeField] private PotionDataVariable targetPotion;
        
        [SerializeField] private ScriptableListIngredientData cauldronIngredients;
        [SerializeField] private ScriptableListIngredientData expectedIngredients;

        [SerializeField] private ScriptableEventNoParam nextPotionEvent;
        [SerializeField] private ScriptableEventPotionData spawnPotionEvent;
        
        private readonly List<IngredientData> ingredientsToCheck = new();
        private readonly List<IngredientData> ingredientsComplete = new();
        
#region Lifecycle Events

        private void OnEnable()
        {
            expectedIngredients.OnCleared += OnExpectedIngredientsCleared;
            expectedIngredients.OnItemsAdded += OnExpectedIngredientsAdded;
            
            cauldronIngredients.OnItemAdded += OnCauldronItemAdded;
        }

        private void OnDisable()
        {
            expectedIngredients.OnCleared -= OnExpectedIngredientsCleared;
            expectedIngredients.OnItemsAdded -= OnExpectedIngredientsAdded;
            
            cauldronIngredients.OnItemAdded -= OnCauldronItemAdded;
        }

#endregion

#region Subscriptions

        private void OnCauldronItemAdded(IngredientData ingredient)
        {
            if (ingredient != targetIngredient.Value)
            {
                ingredientsToCheck.AddRange(cauldronIngredients);
                cauldronIngredients.Clear();
                return;
            }
            
            if (ingredientsToCheck.Contains(ingredient) == false)
            {
                ingredientsToCheck.AddRange(ingredientsComplete);
                cauldronIngredients.Clear();
                return;
            }
            
            ingredientsToCheck.Remove(ingredient);
            ingredientsComplete.Add(ingredient);

            if (ingredientsToCheck.Count == 0)
            {
                cauldronIngredients.Clear();
                
                spawnPotionEvent.Raise(targetPotion.Value);
                nextPotionEvent.Raise();
            }
        }

        private void OnExpectedIngredientsAdded(IEnumerable<IngredientData> ingredients)
        {
            ingredientsComplete.Clear();
            ingredientsToCheck.AddRange(ingredients);
        }

        private void OnExpectedIngredientsCleared()
        {
            ingredientsToCheck.Clear();
        }

#endregion
    }
}