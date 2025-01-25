using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PD_Potion", menuName = "Custom/Data/Potion", order = 0)]
    public class PotionData : CarryableData
    {
        [field: SerializeField] public IngredientData[] Ingredients { get; private set; }

        public int IngredientCount => Ingredients.Length;
    }
}