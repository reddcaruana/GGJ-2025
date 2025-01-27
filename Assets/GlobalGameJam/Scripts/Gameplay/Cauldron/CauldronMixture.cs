using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronMixture
    {
        public event System.Action OnSuccess; 
        public event System.Action OnFailure; 
        
        private IngredientData expected;
        private readonly List<IngredientData> ingredients = new();
        
        private PotionData target;

#region Event Handlers

        public void OnAddedHandler(IngredientData ingredient)
        {
            if (ingredient != expected)
            {
                ingredients.Clear();
                OnFailure?.Invoke();
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
                    OnFailure?.Invoke();
                    return;
                }
            }

            if (required.Count != added.Count)
            {
                Debug.Log("Not complete.");
                return;
            }
            
            ingredients.Clear();
            OnSuccess?.Invoke();
        }

        public void OnExpectedChangedHandler(IngredientData ingredient)
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