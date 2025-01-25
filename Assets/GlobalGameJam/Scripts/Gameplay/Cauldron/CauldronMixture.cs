using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronMixture
    {
        private IngredientData expected;
        private readonly List<IngredientData> ingredients = new();
        
        private PotionData target;

#region Event Handlers

        public void OnIngredientAddedHandler(IngredientData ingredient)
        {
            if (ingredient != expected)
            {
                ingredients.Clear();
                Debug.Log("Bad ingredient (not expected).");
                return;
            }
            ingredients.Add(ingredient);

            var required = target.Ingredients.ToList();
            var added = new List<IngredientData>(ingredients);

            foreach (var item in added)
            {
                if (required.Contains(item) == false)
                {
                    ingredients.Clear();
                    Debug.Log("Bad ingredient (not in recipe).");
                    return;
                }
            }

            if (required.Count != added.Count)
            {
                Debug.Log("Not complete.");
                return;
            }
            
            ingredients.Clear();
            Debug.Log("Complete.");
        }

        public void OnExpectedIngredientChangedHandler(IngredientData ingredient)
        {
            expected = ingredient;
        }

        public void OnTargetPotionChanged(PotionData potion)
        {
            target = potion;
        }

#endregion
    }
}