using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    internal class IngredientAddedListener : MonoBehaviour
    {
        [SerializeField] private ScriptableListIngredientData cauldronIngredients;
        [SerializeField] private ScriptableEventIngredientData ingredientAddedEvent;

#region Lifecycle Events

        private void OnEnable()
        {
            ingredientAddedEvent.OnRaised += OnIngredientAdded;
        }

        private void OnDisable()
        {
            ingredientAddedEvent.OnRaised -= OnIngredientAdded;
        }

#endregion

#region Methods

        private void OnIngredientAdded(IngredientData ingredientData)
        {
            cauldronIngredients.Add(ingredientData);
        }

#endregion
    }
}