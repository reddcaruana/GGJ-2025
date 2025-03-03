using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class IngredientCatcher : MonoBehaviour
    {
        [SerializeField] private ScriptableListIngredientData ingredientList;
        
#region Lifecycle Events

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item") == false)
            {
                return;
            }

            if (other.TryGetComponent(out Item item) == false)
            {
                return;
            }

            if (item.Data is not IngredientData ingredientData)
            {
                return;
            }
            
            ingredientList.Add(ingredientData);
            item.Use();
        }

#endregion
    }
}