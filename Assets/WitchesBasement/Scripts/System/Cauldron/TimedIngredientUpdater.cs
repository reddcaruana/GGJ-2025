using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    internal class TimedIngredientUpdater : MonoBehaviour
    {
        [Header("Registry")]
        [SerializeField] private IngredientRegistry registry;

        [Header("Variables")]
        [SerializeField] private FloatVariable duration;
        [SerializeField] private IngredientDataVariable targetIngredient;

        [Header("Events")]
        [SerializeField] private ScriptableEventIngredientData updateEvent;
        
        [Header("Subscriptions")]
        [SerializeField] private ScriptableEventPotionData scriptableEvent;
        
        private Queue<IngredientData> ingredientQueue;

#region Lifecycle Events

        private void Awake()
        {
            ingredientQueue = new Queue<IngredientData>(registry.Ingredients);
        }

        private void OnEnable()
        {
            scriptableEvent.OnRaised += OnPotionUpdated;
        }

        private void OnDisable()
        {
            scriptableEvent.OnRaised -= OnPotionUpdated;
        }

        private void Start()
        {
            StartCoroutine(IngredientCycleRoutine());
        }

#endregion
        
#region Event Handlers

        private void OnPotionUpdated(PotionData potionData)
        {
            ingredientQueue = new Queue<IngredientData>(potionData.Ingredients);
            targetIngredient.Value = null;
        }

#endregion

#region Coroutines

        private IEnumerator IngredientCycleRoutine()
        {
            while (true)
            {
                if (targetIngredient.Value is not null)
                {
                    ingredientQueue.Enqueue(targetIngredient.Value);
                }

                targetIngredient.Value = ingredientQueue.Dequeue();
                updateEvent.Raise(targetIngredient.Value);

                yield return new WaitForSeconds(duration.Value);
            }
        }

#endregion
    }
}