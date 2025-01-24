using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PD_Potion", menuName = "Custom/Data/Potion", order = 0)]
    public class PotionData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public IngredientData[] Ingredients { get; private set; }

        public int IngredientCount => Ingredients.Length;
        
        public PotionResult Evaluate(IngredientData[] ingredients)
        {
            var requiredIngredients = Ingredients.ToList();
            var addedIngredients = ingredients.ToList();

            foreach (var ingredient in addedIngredients)
            {
                if (requiredIngredients.Contains(ingredient) == false)
                {
                    return PotionResult.Incorrect;
                }

                requiredIngredients.Remove(ingredient);
            }

            if (requiredIngredients.Count > 0)
            {
                return PotionResult.Incomplete;
            }

            return PotionResult.Complete;
        }
    }
}