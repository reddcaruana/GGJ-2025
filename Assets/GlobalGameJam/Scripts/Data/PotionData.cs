using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PD_Potion", menuName = "Custom/Data/Potion", order = 0)]
    public class PotionData : CarryableData
    {
        [field: SerializeField] public IngredientData[] Ingredients { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public float Time { get; private set; }

        public int IngredientCount => Ingredients.Length;
    }
}