using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay.Cauldron
{
    public class CauldronManager : MonoBehaviour
    {
        private ObjectiveManager objectiveManager;

        private List<IngredientData> ingredients;

#region Lifecycle Events

        private void Awake()
        {
            objectiveManager = Singleton.GetOrCreateMonoBehaviour<ObjectiveManager>();
        }

#endregion

#region Methods

        public void Evaluate(IngredientData ingredientData)
        {
            ingredients.Add(ingredientData);
            
        }

#endregion
    }
}