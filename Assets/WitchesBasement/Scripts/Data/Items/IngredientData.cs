using UnityEngine;

namespace WitchesBasement.Data
{
    [CreateAssetMenu(fileName = "ID_Ingredient", menuName = "Custom/Data/Ingredient", order = 0)]
    public class IngredientData : ItemData
    {
        [field: SerializeField] public Sprite CrateLabel { get; private set; }
        [field: SerializeField] public Color Color { get; private set; } = Color.white;
    }
}