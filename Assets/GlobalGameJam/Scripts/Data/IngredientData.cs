using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "ID_Ingredient", menuName = "Custom/Data/Ingredient", order = 0)]
    public class IngredientData : PickableObjectData
    {
        [field: SerializeField] public Color Color { get; private set; } = Color.white;
    }
}