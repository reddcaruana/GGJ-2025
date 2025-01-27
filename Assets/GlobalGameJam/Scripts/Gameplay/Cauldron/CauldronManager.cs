using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronManager : MonoBehaviour
    {
        [Header("Objective Management")]
        [SerializeField] private CauldronObjective cauldronObjective;
        
        [Header("Ingredient Management")]
        [SerializeField] private IngredientCatcher ingredientCatcher;
        [SerializeField] private IngredientSequencer ingredientSequencer;
        [SerializeField] private CauldronContents cauldronContents;
        
        private CauldronContext cauldronContext;

#region Lifecycle Events

        private void Awake()
        {
            cauldronContext = new CauldronContext
            {
                Objective = cauldronObjective,
                
                Catcher = ingredientCatcher,
                Sequencer = ingredientSequencer,
                Contents = cauldronContents,
            };
        }

        private void OnEnable()
        {
            // cauldronContext.Catcher.OnAdded += cauldronContext.Contents.OnAddedHandler;
            //
            // cauldronContext.Mixture.OnSuccess += cauldronContext.Contents.Submerge;
            //
            // cauldronContext.Mixture.OnFailure += cauldronContext.Contents.Submerge;
        }

        private void OnDisable()
        {
            // cauldronContext.Catcher.OnAdded -= cauldronContext.Contents.OnAddedHandler;
            //
            // cauldronContext.Mixture.OnSuccess -= cauldronContext.Contents.Submerge;
            //
            // cauldronContext.Mixture.OnFailure -= cauldronContext.Contents.Submerge;
        }

        private void Start()
        {
            cauldronContext.Objective.Next();
        }

#endregion

#region Methods

        public CauldronContext GetContext()
        {
            return cauldronContext;
        }

#endregion
    }
}