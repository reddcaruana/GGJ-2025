using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the brewing process by listening for expected ingredient changes and handling added ingredients.
    /// </summary>
    public class CauldronBrew : ExpectedIngredientChangeListener
    {
        /// <summary>
        /// The currently expected ingredient.
        /// </summary>
        private IngredientData expected;

        /// <summary>
        /// The list of ingredients that have been added.
        /// </summary>
        private readonly List<IngredientData> ingredients = new();

        /// <summary>
        /// The potion data for a failed potion.
        /// </summary>
        private PotionData failedPotion;

        /// <summary>
        /// The target potion data.
        /// </summary>
        private PotionData target;

        /// <summary>
        /// Event binding for the added ingredient event.
        /// </summary>
        private EventBinding<CauldronEvents.AddedIngredient> onAddedIngredientEventBinding;

        /// <summary>
        /// Event binding for the objective updated event.
        /// </summary>
        private EventBinding<LevelEvents.ObjectiveUpdated> onObjectiveUpdatedEventBinding;

#region Overrides of ExpectedIngredientChangeListener

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            onAddedIngredientEventBinding = new EventBinding<CauldronEvents.AddedIngredient>(OnAddedIngredientEventHandler);
            onObjectiveUpdatedEventBinding = new EventBinding<LevelEvents.ObjectiveUpdated>(OnObjectiveUpdatedEventHandler);

            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            failedPotion = potionRegistry.Pootion;
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            base.OnEnable();
            EventBus<CauldronEvents.AddedIngredient>.Register(onAddedIngredientEventBinding);
            EventBus<LevelEvents.ObjectiveUpdated>.Register(onObjectiveUpdatedEventBinding);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            EventBus<CauldronEvents.AddedIngredient>.Deregister(onAddedIngredientEventBinding);
            EventBus<LevelEvents.ObjectiveUpdated>.Deregister(onObjectiveUpdatedEventBinding);
        }

        /// <inheritdoc />
        protected override void OnChangedExpectedIngredientHandler(CauldronEvents.ChangedExpectedIngredient @event)
        {
            expected = @event.Ingredient;
        }

#endregion

#region Methods

        /// <summary>
        /// Handles the failure of the brewing process by clearing ingredients and raising a failure event.
        /// </summary>
        private void Fail()
        {
            ingredients.Clear();
            EventBus<CauldronEvents.EvaluatePotion>.Raise(new CauldronEvents.EvaluatePotion
            {
                Outcome = OutcomeType.Failure,
                Potion = failedPotion
            });
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when an ingredient is added.
        /// </summary>
        /// <param name="event">The added ingredient event data.</param>
        private void OnAddedIngredientEventHandler(CauldronEvents.AddedIngredient @event)
        {
            if (@event.Ingredient != expected)
            {
                Fail();
                return;
            }

            ingredients.Add(@event.Ingredient);

            var required = target.Ingredients.ToList();
            var added = new List<IngredientData>(ingredients);

            foreach (var item in added)
            {
                if (required.Contains(item) == false)
                {
                    Fail();
                    return;
                }

                required.Remove(item);
            }

            // Not complete
            if (required.Count > 0)
            {
                EventBus<CauldronEvents.EmergedIngredient>.Raise(new CauldronEvents.EmergedIngredient()
                {
                    Ingredient = @event.Ingredient
                });
                return;
            }

            ingredients.Clear();
            EventBus<CauldronEvents.EvaluatePotion>.Raise(new CauldronEvents.EvaluatePotion
            {
                Outcome = OutcomeType.Success,
                Potion = target
            });
        }

        /// <summary>
        /// Handles the event when an objective is updated.
        /// </summary>
        /// <param name="event">The objective updated event data.</param>
        private void OnObjectiveUpdatedEventHandler(LevelEvents.ObjectiveUpdated @event)
        {
            target = @event.Potion;
        }

#endregion
    }
}