using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the contents of the cauldron, handling ingredient addition and potion evaluation.
    /// </summary>
    public class CauldronContents : MonoBehaviour
    {
        /// <summary>
        /// Array of ingredients in the cauldron.
        /// </summary>
        [SerializeField] private CauldronIngredient[] ingredients;

        /// <summary>
        /// The current index of the ingredient being added.
        /// </summary>
        private int index;

        /// <summary>
        /// Event binding for handling the addition of an ingredient.
        /// </summary>
        private EventBinding<CauldronEvents.EmergedIngredient> onEmergedIngredientEventBinding;

        /// <summary>
        /// Event binding for handling the evaluation of the potion.
        /// </summary>
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes event bindings.
        /// </summary>
        private void Awake()
        {
            onEmergedIngredientEventBinding = new EventBinding<CauldronEvents.EmergedIngredient>(OnEmergedIngredientEventHandler);
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        /// <summary>
        /// Registers event bindings when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.EmergedIngredient>.Register(onEmergedIngredientEventBinding);
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        /// <summary>
        /// Deregisters event bindings when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<CauldronEvents.EmergedIngredient>.Deregister(onEmergedIngredientEventBinding);
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
        }

        /// <summary>
        /// Sets random animation offsets for each ingredient at the start.
        /// </summary>
        private void Start()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.SetAnimationOffset(Random.value * 2.0f);
            }
        }

        /// <summary>
        /// Resets the ingredients array.
        /// </summary>
        private void Reset()
        {
            ingredients = GetComponentsInChildren<CauldronIngredient>();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when an ingredient is to be added to the cauldron.
        /// </summary>
        /// <param name="event">The added ingredient event.</param>
        private void OnEmergedIngredientEventHandler(CauldronEvents.EmergedIngredient @event)
        {
            if (index >= ingredients.Length)
            {
                return;
            }

            var ingredient = ingredients[index];
            ingredient.Emerge(@event.Ingredient);

            index++;
        }

        /// <summary>
        /// Handles the event when the potion is evaluated.
        /// </summary>
        /// <param name="event">The evaluate potion event.</param>
        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            for (var i = 0; i < index; i++)
            {
                var ingredient = ingredients[i];
                ingredient.Submerge();
            }

            index = 0;
        }

#endregion
    }
}