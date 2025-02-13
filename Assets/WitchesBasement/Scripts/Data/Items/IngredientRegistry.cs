using UnityEngine;

namespace WitchesBasement.Data
{
    [CreateAssetMenu(fileName = "REG_Ingredients", menuName = "Custom/Registries/Ingredients", order = 0)]
    public class IngredientRegistry : ScriptableObject
    {
        [field: SerializeField] public IngredientData[] Ingredients { get; private set; }

#region Context Menu

        [ContextMenu("Find Ingredients")]
        private void FindIngredients()
        {
            Ingredients = Resources.LoadAll<IngredientData>("Ingredients");
        }

#endregion
    }
}