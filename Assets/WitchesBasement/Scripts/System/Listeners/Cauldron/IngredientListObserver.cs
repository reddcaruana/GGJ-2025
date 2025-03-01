using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class IngredientListObserver : MonoBehaviour
    {
        [SerializeField] private ScriptableListIngredientData cauldronIngredients;
        [SerializeField] private IngredientVisual[] ingredientVisuals;

        private int currentIndex;
        
#region Lifecycle Events

        private void Start()
        {
            currentIndex = 0;
        }

        private void OnEnable()
        {
            cauldronIngredients.OnCleared += OnIngredientsCleared;
            cauldronIngredients.OnItemAdded += OnIngredientAdded;
        }

        private void OnDisable()
        {
            cauldronIngredients.OnCleared -= OnIngredientsCleared;
            cauldronIngredients.OnItemAdded -= OnIngredientAdded;
        }

        private void Reset()
        {
            ingredientVisuals = GetComponentsInChildren<IngredientVisual>();
        }

#endregion

#region Methods
        
        private void OnIngredientAdded(IngredientData ingredientData)
        {
            ingredientVisuals[currentIndex].Emerge(ingredientData);
            currentIndex++;
        }

        private void OnIngredientsCleared()
        {
            for (var i = 0; i < currentIndex; i++)
            {
                ingredientVisuals[i].Submerge();
            }

            currentIndex = 0;
        }

#endregion
    }
}