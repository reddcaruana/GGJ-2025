using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using GlobalGameJam.Lobby;
using GlobalGameJam.UI;
using UnityEngine;
using UnityEngine.Playables;

namespace GlobalGameJam.Level
{
    public struct LevelContext
    {
        public PlayerBehavior[] PlayerBehaviors;

        public LoginScreen LoginScreen; 
            
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
        [SerializeField] private LoginScreen loginScreen;
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
                LoginScreen = loginScreen,
                
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
            if (index >= 3)
            {
                timeline.Play();
            }
        }

        public void Begin()
        {
            levelContext.LoginScreen.Deactivate();
            
            levelContext.GameTimer.Activate();
            levelContext.GameTimer.OnComplete += OnTimerCompleteHandler;
            OnLevelStart?.Invoke();

            for (var i = 0; i < playerBehaviors.Length; i++)
            {
                playerBehaviors[i].Bind(i);
            }
        }

#endregion

#region Event Handlers

        private void OnTimerCompleteHandler()
        {
            OnLevelStop?.Invoke();
            levelContext.Timeline.Play();

            foreach (var player in playerBehaviors)
            {
                player.Release();
            }
        }

#endregion
    }
}