using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the sequencing of ingredients, cycling through them at specified intervals.
    /// </summary>
    public class IngredientSequencer : MonoBehaviour
    {
        /// <summary>
        /// The duration of each cycle in seconds.
        /// </summary>
        [Header("Timing Values")] [SerializeField]
        private float cycleDuration = 1.75f;

        /// <summary>
        /// The duration of the color change effect in seconds.
        /// </summary>
        [SerializeField] private float colorChangeDuration = 0.15f;

        /// <summary>
        /// The current ingredient being processed.
        /// </summary>
        private IngredientData current;

        /// <summary>
        /// A queue of ingredients to cycle through.
        /// </summary>
        private Queue<IngredientData> ingredientQueue;

#region Lifecycle Events

        /// <summary>
        /// Initializes the ingredient queue with ingredients from the registry.
        /// </summary>
        private void Awake()
        {
            var registry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            ingredientQueue = new Queue<IngredientData>(registry.Ingredients);
        }

        /// <summary>
        /// Starts the ingredient cycling coroutine.
        /// </summary>
        private void Start()
        {
            StartCoroutine(IngredientCycleRoutine());
        }

#endregion

#region Coroutines

        /// <summary>
        /// Coroutine that cycles through ingredients, raising an event each cycle.
        /// </summary>
        /// <returns>An enumerator for the coroutine.</returns>
        private IEnumerator IngredientCycleRoutine()
        {
            while (true)
            {
                if (current is not null)
                {
                    ingredientQueue.Enqueue(current);
                }

                current = ingredientQueue.Dequeue();
                EventBus<CauldronEvents.ChangedExpectedIngredient>.Raise(new CauldronEvents.ChangedExpectedIngredient
                {
                    Ingredient = current,
                    ColorChangeDuration = colorChangeDuration
                });

                yield return new WaitForSeconds(cycleDuration);
            }
        }

#endregion
    }
}