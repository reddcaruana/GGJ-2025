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
        
        [Header("Potion Returning")]
        [SerializeField] private Transform throwAnchor;
        [SerializeField] private float throwSpeed = 10.0f;
        [SerializeField] private float throwAngle = 45.0f;
        [SerializeField] private Direction throwDirection = Direction.South;
        
        private CauldronContext cauldronContext;
        private Throw cauldronThrow;

#region Lifecycle Events

        private void Awake()
        {
            cauldronContext = new CauldronContext
            {
                Mixture = new CauldronMixture(),
                Objective = cauldronObjective,
                
                Catcher = ingredientCatcher,
                Sequencer = ingredientSequencer,
                Contents = cauldronContents,
                
                PotionAnchor = throwAnchor,
                Throw = new Throw(throwSpeed, throwAngle)
            };
        }

        private void OnEnable()
        {
            cauldronContext.Objective.OnChanged += cauldronContext.Mixture.OnTargetPotionChanged;
            
            cauldronContext.Catcher.OnAdded += cauldronContext.Mixture.OnAddedHandler;
            cauldronContext.Catcher.OnAdded += cauldronContext.Contents.OnAddedHandler;
            
            cauldronContext.Mixture.OnSuccess += OnMixtureSuccess;
            cauldronContext.Mixture.OnSuccess += cauldronContext.Contents.Submerge;
            
            cauldronContext.Mixture.OnFailure += OnMixtureFailure;
            cauldronContext.Mixture.OnFailure += cauldronContext.Contents.Submerge;
        }

        private void OnDisable()
        {
            cauldronContext.Objective.OnChanged -= cauldronContext.Mixture.OnTargetPotionChanged;

            cauldronContext.Catcher.OnAdded -= cauldronContext.Mixture.OnAddedHandler;
            cauldronContext.Catcher.OnAdded -= cauldronContext.Contents.OnAddedHandler;

            cauldronContext.Mixture.OnSuccess -= OnMixtureSuccess;
            cauldronContext.Mixture.OnSuccess -= cauldronContext.Contents.Submerge;
            
            cauldronContext.Mixture.OnFailure -= OnMixtureFailure;
            cauldronContext.Mixture.OnFailure -= cauldronContext.Contents.Submerge;
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

        private void SpawnPotion(PotionData potionData)
        {
            var potionManager = Singleton.GetOrCreateMonoBehaviour<PotionManager>();

            var potion = potionManager.Generate(potionData, cauldronContext.PotionAnchor);
            potion.Throw(throwDirection.ToVector(), throwSpeed, throwAngle);

            cauldronContext.Objective.Next();
        }

#endregion

#region Event Handlers

        private void OnMixtureSuccess()
        {
            var data = cauldronContext.Objective.Target;
            SpawnPotion(data);
        }

        private void OnMixtureFailure()
        {
            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            SpawnPotion(potionRegistry.Pootion);
        }

#endregion
    }
}