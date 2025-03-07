using GlobalGameJam.Data;

namespace GlobalGameJam.Events
{
    public static class CauldronEvents
    {
        public struct AddedIngredient : IEvent
        {
            public IngredientData Ingredient;
        }
        
        public struct ChangedExpectedIngredient : IEvent
        {
            public IngredientData Ingredient;
            public float ColorChangeDuration;
        }

        public struct EmergedIngredient : IEvent
        {
            public IngredientData Ingredient;
        }

        public struct EvaluatePotion : IEvent
        {
            public OutcomeType Outcome; 
            public PotionData Potion;
        }
    }
}