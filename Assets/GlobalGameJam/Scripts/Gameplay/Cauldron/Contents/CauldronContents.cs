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
        /// The outcome of the potion evaluation.
        /// </summary>
        private OutcomeType evaluationOutcome = OutcomeType.None;

        /// <summary>
        /// The current index of the ingredient being added.
        /// </summary>
        private int index;

        /// <summary>
        /// Event binding for handling the addition of an ingredient.
        /// </summary>
        private EventBinding<CauldronEvents.AddedIngredient> onAddedIngredientEventBinding;

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
            onAddedIngredientEventBinding = new EventBinding<CauldronEvents.AddedIngredient>(OnAddedIngredientEventHandler);
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        /// <summary>
        /// Registers event bindings when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.AddedIngredient>.Register(onAddedIngredientEventBinding);
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        /// <summary>
        /// Deregisters event bindings when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<CauldronEvents.AddedIngredient>.Deregister(onAddedIngredientEventBinding);
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
        /// Handles the event when an ingredient is added to the cauldron.
        /// </summary>
        /// <param name="event">The added ingredient event.</param>
        private void OnAddedIngredientEventHandler(CauldronEvents.AddedIngredient @event)
        {
            if (evaluationOutcome is not OutcomeType.None)
            {
                return;
            }

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
            evaluationOutcome = @event.Outcome;
            if (evaluationOutcome is OutcomeType.None)
            {
                return;
            }

            for (var i = 0; i < index; i++)
            {
                var ingredient = ingredients[i];
                ingredient.Submerge();
            }

            index = 0;
            evaluationOutcome = OutcomeType.None;
        }

#endregion
    }
}