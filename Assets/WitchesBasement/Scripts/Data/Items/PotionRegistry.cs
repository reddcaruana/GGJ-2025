using System.Linq;
using UnityEngine;

namespace WitchesBasement.Data
{
    [CreateAssetMenu(fileName = "REG_Potions", menuName = "Custom/Registries/Potions", order = 0)]
    public class PotionRegistry : ScriptableObject
    {
        [field: SerializeField] public PotionData Pootion { get; private set; }
        [field: SerializeField] public PotionData[] Potions { get; private set; }

        public int MaxIngredientCount => Potions.Max(potion => potion.IngredientCount);
    }
}