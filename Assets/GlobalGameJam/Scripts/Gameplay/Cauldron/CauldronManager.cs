using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronManager : MonoBehaviour
    {
        [SerializeField] private float duration = 3f;
        
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

        public void Brew()
        {
            Debug.Log("Potion being brewed");
        }

        public void Evaluate(IngredientData ingredientData)
        {
            ingredients.Add(ingredientData);
            var state = targetPotion.Evaluate(ingredients.ToArray());

            switch (state)
            {
                case PotionResult.Incorrect:
                    Fail();
                    break;
                
                case PotionResult.Complete:
                    Brew();
                    break;
            }
        }

        public void Fail()
        {
            Debug.Log("Potion failed");
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