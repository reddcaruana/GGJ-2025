using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public struct CauldronContext
    {
        public CauldronMixture CauldronMixture;
        public CauldronObjective CauldronObjective;
        
        public IngredientCatcher IngredientCatcher;
        public IngredientSequencer IngredientSequencer;

        public Transform ThrowAnchor;
        public Throw CauldronThrow;
    }
}