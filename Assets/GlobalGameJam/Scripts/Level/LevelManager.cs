using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using GlobalGameJam.UI;
using UnityEngine;

namespace GlobalGameJam.Level
{
    public struct LevelContext
    {
        public PlayerBehavior[] PlayerBehaviors;
        
        public CauldronManager CauldronManager;

        public ObjectiveDisplay ObjectiveDisplay;

        public TimerDisplay TimerDisplay;
        
        public Timer GameTimer;

        public ChestBatch[] ChestBatches;
    }
    
    public class LevelManager : MonoBehaviour
    {
        public event System.Action OnLevelStart;
        public event System.Action OnLevelStop;
        
        [SerializeField] private PlayerBehavior[] playerBehaviors;
        [SerializeField] private ChestBatch[] chestBatches;
        [SerializeField] private CauldronManager cauldronManager;
        [SerializeField] private ObjectiveDisplay objectiveDisplay;
        [SerializeField] private TimerDisplay timerDisplay;
        [SerializeField] private Timer gameTimer;

        private LevelContext levelContext;
        
#region Lifecycle Events

        private void Awake()
        {
            levelContext = new LevelContext
            {
                PlayerBehaviors = playerBehaviors,
                ChestBatches = chestBatches,
                CauldronManager = cauldronManager,
                ObjectiveDisplay = objectiveDisplay,
                TimerDisplay = timerDisplay,
                GameTimer = gameTimer
            };
        }

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerDataManager.OnPlayerJoined += (id) =>
            {
                levelContext.PlayerBehaviors[id].Bind(id);
            };

            var ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            
            var randomIndex = Random.Range(0, chestBatches.Length);
            chestBatches[randomIndex].gameObject.SetActive(true);
            chestBatches[randomIndex].SetChests(ingredientRegistry.Ingredients);
            
            OnLevelStart?.Invoke();
            
            levelContext.ObjectiveDisplay.Bind(cauldronManager.GetContext());
            levelContext.TimerDisplay.Bind(levelContext.GameTimer);
            levelContext.GameTimer.Activate();
        }

        private void OnDestroy()
        {
            OnLevelStop?.Invoke();
        }

#endregion
    }
}