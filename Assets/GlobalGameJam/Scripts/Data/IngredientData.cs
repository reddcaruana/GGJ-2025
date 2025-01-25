using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "ID_Ingredient", menuName = "Custom/Data/Ingredient", order = 0)]
    public class IngredientData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Color Color { get; private set; } = Color.white;
    }
}