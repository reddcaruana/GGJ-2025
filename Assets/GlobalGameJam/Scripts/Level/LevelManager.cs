using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using GlobalGameJam.UI;
using UnityEngine;
using UnityEngine.Playables;

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

        public ScoreManager Score;
        public LeaderboardManager Leaderboard;

        public ShippingBin ShippingBin;

        public PlayableDirector Timeline;
    }
    
    public class LevelManager : MonoBehaviour
    {
        public event System.Action OnLevelStart;
        public event System.Action OnLevelStop;
        
        [SerializeField] private PlayerBehavior[] playerBehaviors;
        [SerializeField] private ChestBatch[] chestBatches;
        [SerializeField] private CauldronManager cauldronManager;
        [SerializeField] private ObjectiveDisplay objectiveDisplay;
        [SerializeField] private ScoreManager score;
        [SerializeField] private LeaderboardManager leaderboard;
        [SerializeField] private PlayableDirector timeline;
        [SerializeField] private ShippingBin shippingBin;
        [SerializeField] private TimerDisplay timerDisplay;
        [SerializeField] private Timer gameTimer;

        private LevelContext levelContext;

        private SessionOutcome sessionOutcome;
        
#region Lifecycle Events

        private void Awake()
        {
            levelContext = new LevelContext
            {
                PlayerBehaviors = playerBehaviors,
                ChestBatches = chestBatches,
                
                CauldronManager = cauldronManager,
                ShippingBin = shippingBin,
                
                ObjectiveDisplay = objectiveDisplay,
                TimerDisplay = timerDisplay,
                
                Score = score,
                Leaderboard = leaderboard,
                
                Timeline = timeline,    
                
                GameTimer = gameTimer
            };
        }

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            var ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            var randomIndex = Random.Range(0, chestBatches.Length);
            chestBatches[randomIndex].gameObject.SetActive(true);
            chestBatches[randomIndex].SetChests(ingredientRegistry.Ingredients);
            
            levelContext.Score.Bind(levelContext.ShippingBin);
            
            levelContext.ObjectiveDisplay.Bind(cauldronManager.GetContext());
            levelContext.TimerDisplay.Bind(levelContext.GameTimer);
        }

#endregion

#region Methods

        public void AddPlayer(int index)
        {
            Debug.Log(index);
            if (index == 0)
            {
                timeline.Play();
            }
        }

#endregion
    }
}