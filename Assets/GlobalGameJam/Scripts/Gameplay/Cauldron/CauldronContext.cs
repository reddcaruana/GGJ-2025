using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public struct CauldronContext
    {
        public CauldronObjective Objective;
        public CauldronContents Contents;
        
        public IngredientCatcher Catcher;
        public IngredientSequencer Sequencer;
    }
}