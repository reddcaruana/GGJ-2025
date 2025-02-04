using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;
using GlobalGameJam.Events;
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

        /// <summary>
        /// Event binding for handling objective updated events.
        /// </summary>
        private EventBinding<LevelEvents.ObjectiveUpdated> onObjectiveUpdatedEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the ingredient queue with ingredients from the registry.
        /// </summary>
        private void Awake()
        {
            var registry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            ingredientQueue = new Queue<IngredientData>(registry.Ingredients);

            onObjectiveUpdatedEventBinding = new EventBinding<LevelEvents.ObjectiveUpdated>(OnObjectiveUpdatedEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.ObjectiveUpdated>.Register(onObjectiveUpdatedEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.ObjectiveUpdated>.Deregister(onObjectiveUpdatedEventBinding);
        }

        /// <summary>
        /// Starts the ingredient cycling coroutine.
        /// </summary>
        private void Start()
        {
            StartCoroutine(IngredientCycleRoutine());
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when an objective is updated.
        /// </summary>
        /// <param name="event">The objective updated event.</param>
        private void OnObjectiveUpdatedEventHandler(LevelEvents.ObjectiveUpdated @event)
        {
            var ingredients = @event.Potion.Ingredients;

            ingredientQueue = new Queue<IngredientData>(ingredients);
            current = null;
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