using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronManager : MonoBehaviour
    {
        [SerializeField] private CauldronObjective cauldronObjective;
        [SerializeField] private IngredientCatcher ingredientCatcher;
        [SerializeField] private IngredientSequencer ingredientSequencer;
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
                CauldronMixture = new CauldronMixture(),
                CauldronObjective = cauldronObjective,
                CauldronThrow = new Throw(throwSpeed, throwAngle),
                
                IngredientCatcher = ingredientCatcher,
                IngredientSequencer = ingredientSequencer,
                
                ThrowAnchor = throwAnchor
            };
        }

        private void OnEnable()
        {
            cauldronContext.CauldronObjective.OnChanged += cauldronContext.CauldronMixture.OnTargetPotionChanged;
            cauldronContext.IngredientSequencer.OnChanged += cauldronContext.CauldronMixture.OnExpectedIngredientChangedHandler;
            cauldronContext.IngredientCatcher.OnAdded += cauldronContext.CauldronMixture.OnIngredientAddedHandler;

            cauldronContext.CauldronMixture.OnSuccess += OnMixtureSuccess;
            cauldronContext.CauldronMixture.OnFailure += OnMixtureFailure;
        }

        private void OnDisable()
        {
            cauldronContext.CauldronObjective.OnChanged -= cauldronContext.CauldronMixture.OnTargetPotionChanged;
            cauldronContext.IngredientSequencer.OnChanged -= cauldronContext.CauldronMixture.OnExpectedIngredientChangedHandler;
            cauldronContext.IngredientCatcher.OnAdded -= cauldronContext.CauldronMixture.OnIngredientAddedHandler;
            
            cauldronContext.CauldronMixture.OnSuccess -= OnMixtureSuccess;
            cauldronContext.CauldronMixture.OnFailure -= OnMixtureFailure;
        }

        private void Start()
        {
            cauldronContext.CauldronObjective.Next();
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

            var potion = potionManager.Generate(potionData, cauldronContext.ThrowAnchor);
            potion.Throw(throwDirection.ToVector(), throwSpeed, throwAngle);

            cauldronContext.CauldronObjective.Next();
        }

#endregion

#region Event Handlers

        private void OnMixtureSuccess()
        {
            var data = cauldronContext.CauldronObjective.Target;
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