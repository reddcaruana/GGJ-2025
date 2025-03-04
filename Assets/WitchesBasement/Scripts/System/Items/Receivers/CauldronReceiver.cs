using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class CauldronReceiver : ItemReceiverBase<IngredientData>
    {
        [SerializeField] private ScriptableListIngredientData ingredientList;
        
#region Lifecycle Events

        private void OnTriggerEnter(Collider other)
        {
            if (Validate(other, out var item, out var data) == false)
            {
                return;
            }
            
            ingredientList.Add(data);
            item.Use();
        }

#endregion
    }
}