using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public struct CauldronContext
    {
        public CauldronMixture Mixture;
        public CauldronObjective Objective;
        public CauldronContents Contents;
        
        public IngredientCatcher Catcher;
        public IngredientSequencer Sequencer;

        public Transform PotionAnchor;
        public Throw Throw;
    }
}