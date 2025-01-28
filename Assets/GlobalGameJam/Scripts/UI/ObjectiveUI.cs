using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Displays the current objective, including the target potion and its ingredients.
    /// </summary>
    public class ObjectiveUI : MonoBehaviour
    {
        /// <summary>
        /// The image component used to display the potion.
        /// </summary>
        [SerializeField] private ObjectiveItemUI potionTarget;

        /// <summary>
        /// The array of image components used to display the ingredients.
        /// </summary>
        [SerializeField] private ObjectiveItemUI[] ingredientTargets;

        /// <summary>
        /// Event binding for the objective updated event.
        /// </summary>
        private EventBinding<LevelEvents.ObjectiveUpdated> onObjectiveUpdatedEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the objective updated event.
        /// </summary>
        private void Awake()
        {
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

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the objective updated event by updating the displayed potion and ingredients.
        /// </summary>
        /// <param name="event">The objective updated event data.</param>
        private void OnObjectiveUpdatedEventHandler(LevelEvents.ObjectiveUpdated @event)
        {
            if (@event.Potion is null)
            {
                Debug.Log("Potion will not be displayed because it is null.");
                return;
            }

            StartCoroutine(OnChangeObjectiveRoutine(@event.Potion));
        }

#endregion

#region Coroutines

        /// <summary>
        /// Coroutine to handle the change of the objective by updating the displayed potion and ingredients.
        /// </summary>
        /// <param name="potion">The potion data containing the target potion and its ingredients.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator OnChangeObjectiveRoutine(PotionData potion)
        {
            const float objectiveRefreshInterval = 0.9f;
            const float componentInterval = 0.1f;
            const float componentIntervalNoise = 0.1f;

            var components = new List<ObjectiveItemUI>(ingredientTargets) { potionTarget };
            var indices = Enumerable.Range(0, components.Count).OrderBy(_ => Random.value).ToArray();

            foreach (var index in indices)
            {
                Debug.Log("Clearing ingredient");
                components[index].gameObject.SetActive(true);
                components[index].ClearSprite();

                var interval = componentInterval + Random.value * componentIntervalNoise;
                yield return new WaitForSeconds(interval);
            }

            yield return objectiveRefreshInterval;

            components.Remove(potionTarget);
            indices = Enumerable.Range(0, components.Count).OrderBy(_ => Random.value).ToArray();

            foreach (var index in indices)
            {
                Debug.Log("Setting ingredient");
                if (index >= potion.IngredientCount)
                {
                    components[index].gameObject.SetActive(false);
                    continue;
                }

                components[index].SetSprite(potion.Ingredients[index].Sprite);

                var interval = componentInterval + Random.value * componentIntervalNoise;
                yield return new WaitForSeconds(interval);
            }

            potionTarget.SetSprite(potion.Sprite);
        }

#endregion
    }
}