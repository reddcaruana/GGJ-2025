using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class Brew : ExpectedIngredientChangeListener
    {
        private IngredientData expected;
        private readonly List<IngredientData> ingredients = new();

        private PotionData failedPotion;
        private PotionData target;

        private EventBinding<CauldronEvents.AddedIngredient> onAddedIngredientBinding;
        
#region Overrides of ExpectedIngredientChangeListener

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            onAddedIngredientBinding = new EventBinding<CauldronEvents.AddedIngredient>(OnAddedIngredientHandler);

            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            failedPotion = potionRegistry.Pootion;
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            base.OnEnable();
            EventBus<CauldronEvents.AddedIngredient>.Register(onAddedIngredientBinding);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            EventBus<CauldronEvents.AddedIngredient>.Deregister(onAddedIngredientBinding);
        }

        /// <inheritdoc />
        protected override void OnChangedExpectedIngredientHandler(CauldronEvents.ChangedExpectedIngredient @event)
        {
            expected = @event.Ingredient;
        }

#endregion

#region Methods

        private void Fail()
        {
            ingredients.Clear();
            EventBus<CauldronEvents.ConcoctedPotion>.Raise(new CauldronEvents.ConcoctedPotion
            {
                Outcome = OutcomeType.Failure,
                Potion = failedPotion
            });
        }

#endregion

#region Event Handlers
        
        private void OnAddedIngredientHandler(CauldronEvents.AddedIngredient @event)
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
            if (required.Count < added.Count)
            {
                return;
            }
            
            ingredients.Clear();
            EventBus<CauldronEvents.ConcoctedPotion>.Raise(new CauldronEvents.ConcoctedPotion
            {
                Outcome = OutcomeType.Success,
                Potion = target
            });
        }

        public void OnTargetPotionChanged(PotionData potion)
        {
            target = potion;
        }

#endregion
    }
}