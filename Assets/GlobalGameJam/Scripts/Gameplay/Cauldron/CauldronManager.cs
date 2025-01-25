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

            cauldronContext.CauldronMixture.OnSuccess += Spawn;
        }

        private void OnDisable()
        {
            cauldronContext.CauldronObjective.OnChanged -= cauldronContext.CauldronMixture.OnTargetPotionChanged;
            cauldronContext.IngredientSequencer.OnChanged -= cauldronContext.CauldronMixture.OnExpectedIngredientChangedHandler;
            cauldronContext.IngredientCatcher.OnAdded -= cauldronContext.CauldronMixture.OnIngredientAddedHandler;
            
            cauldronContext.CauldronMixture.OnSuccess -= Spawn;
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

        public void Spawn()
        {
            var data = cauldronContext.CauldronObjective.Target;
            var potionManager = Singleton.GetOrCreateMonoBehaviour<PotionManager>();

            var potion = potionManager.Generate(data, cauldronContext.ThrowAnchor);
            potion.Throw(throwDirection.ToVector(), throwSpeed, throwAngle);

            cauldronContext.CauldronObjective.Next();
        }

#endregion
    }
}