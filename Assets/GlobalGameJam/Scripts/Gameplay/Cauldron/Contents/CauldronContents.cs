using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronContents : MonoBehaviour
    {
        [SerializeField] private CauldronIngredient[] ingredients;

        private OutcomeType evaluationOutcome = OutcomeType.None;
        private int index;

        private EventBinding<CauldronEvents.AddedIngredient> onAddedIngredientEventBinding;
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;
        
#region Lifecycle Events

        private void Awake()
        {
            onAddedIngredientEventBinding = new EventBinding<CauldronEvents.AddedIngredient>(OnAddedIngredientEventHandler);
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        private void OnEnable()
        {
            EventBus<CauldronEvents.AddedIngredient>.Register(onAddedIngredientEventBinding);
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CauldronEvents.AddedIngredient>.Deregister(onAddedIngredientEventBinding);
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
        }

        private void Start()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.SetAnimationOffset(Random.value * 2.0f);
            }
        }

        private void Reset()
        {
            ingredients = GetComponentsInChildren<CauldronIngredient>();
        }

#endregion

#region Event Handlers

        private void OnAddedIngredientEventHandler(CauldronEvents.AddedIngredient @event)
        {
            if (index >= ingredients.Length)
            {
                return;
            }

            if (evaluationOutcome is not OutcomeType.None)
            {
                return;
            }

            var ingredient = ingredients[index];
            ingredient.Emerge(@event.Ingredient);
            
            index++;
        }

        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            evaluationOutcome = @event.Outcome;
            if (evaluationOutcome is OutcomeType.None)
            {
                return;
            }

            for (var i = 0; i < index; i++)
            {
                var ingredient = ingredients[index];
                ingredient.Submerge();
            }

            index = 0;
        }

#endregion
    }
}