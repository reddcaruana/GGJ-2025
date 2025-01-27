using GlobalGameJam.Gameplay;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Image potionImage;

        /// <summary>
        /// The array of image components used to display the ingredients.
        /// </summary>
        [SerializeField] private Image[] ingredientImages;

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
                Debug.Log($"Potion will not be displayed because it is null.");
                return;
            }

            potionImage.sprite = @event.Potion.Sprite;
            potionImage.preserveAspect = true;

            for (var i = 0; i < ingredientImages.Length; i++)
            {
                if (i >= @event.Potion.Ingredients.Length)
                {
                    ingredientImages[i].sprite = null;
                    ingredientImages[i].color = Color.clear;
                    continue;
                }

                ingredientImages[i].sprite = @event.Potion.Ingredients[i].Sprite;
                ingredientImages[i].color = Color.white;
            }
        }

#endregion
    }
}