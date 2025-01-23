using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PD_Ingredient", menuName = "Custom/Data/Potion", order = 0)]
    public class PotionData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public IngredientData[] Ingredients { get; private set; }
    }
}