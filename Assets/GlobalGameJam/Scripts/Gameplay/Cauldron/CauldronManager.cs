using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronManager : MonoBehaviour
    {
        private ObjectiveManager objectiveManager;

        private PotionData targetPotion;
        private readonly List<IngredientData> ingredients = new();

#region Lifecycle Events

        private void Awake()
        {
            objectiveManager = Singleton.GetOrCreateMonoBehaviour<ObjectiveManager>();
        }

        private void OnEnable()
        {
            objectiveManager.OnChanged += OnChangedHandler;
            objectiveManager.Next();
        }

        private void OnDisable()
        {
            objectiveManager.OnChanged -= OnChangedHandler;
        }

#endregion

#region Methods

        public void Evaluate(IngredientData ingredientData)
        {
            ingredients.Add(ingredientData);
            var state = targetPotion.Evaluate(ingredients.ToArray());

            switch (state)
            {
                case PotionResult.Incorrect:
                    Debug.Log("This ingredient is incorrect.");
                    break;
                
                case PotionResult.Complete:
                    Debug.Log("The potion is complete.");
                    break;
            }
        }

#endregion

#region Event Handlers

        private void OnChangedHandler(PotionData potionData)
        {
            targetPotion = potionData;
            ingredients.Clear();
        }

#endregion
    }
}