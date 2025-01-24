using System.Collections.Generic;
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
        
        public bool IsComplete(IngredientData[] ingredients)
        {
            if (ingredients.Length != IngredientCount)
            {
                return false;
            }

            var targetList = new List<IngredientData>(Ingredients);
            foreach (var ingredient in ingredients)
            {
                var index = targetList.IndexOf(ingredient);
                if (index == -1)
                {
                    return false;
                }

                targetList.RemoveAt(index);
            }

            return targetList.Count == 0;
        }
    }
}