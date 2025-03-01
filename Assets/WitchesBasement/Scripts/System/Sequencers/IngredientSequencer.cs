using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    internal class IngredientSequencer : MonoBehaviour
    {
        [Header("Registry")]
        [SerializeField] private IngredientRegistry registry;

        [Header("Variables")]
        [SerializeField] private FloatVariable duration;
        [SerializeField] private IngredientDataVariable targetIngredient;

        [Header("Events")]
        [SerializeField] private ScriptableEventIngredientData ingredientChangedEvent;
        
        [Header("Subscriptions")]
        [SerializeField] private ScriptableEventPotionData potionUpdateEvent;
        
        private Queue<IngredientData> ingredientQueue;

#region Lifecycle Events

        private void Awake()
        {
            ingredientQueue = new Queue<IngredientData>(registry.Ingredients);
        }

        private void OnEnable()
        {
            potionUpdateEvent.OnRaised += OnPotionChanged;
        }

        private void OnDisable()
        {
            potionUpdateEvent.OnRaised -= OnPotionChanged;
        }

        private void Start()
        {
            StartCoroutine(IngredientCycleRoutine());
        }

#endregion
        
#region Event Handlers

        private void OnPotionChanged(PotionData potionData)
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
                ingredientChangedEvent.Raise(targetIngredient.Value);

                yield return new WaitForSeconds(duration.Value);
            }
        }

#endregion
    }
}