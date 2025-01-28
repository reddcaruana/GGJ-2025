using GlobalGameJam.Data;
using GlobalGameJam.Lobby;
using GlobalGameJam.UI;
using UnityEngine;
using UnityEngine.Playables;

namespace GlobalGameJam.Gameplay
{
    public struct LevelContext
    {
        public PlayerBehavior[] PlayerBehaviors;

        public LoginScreen LoginScreen; 
            
        public ChestBatch[] ChestBatches;

        public Leaderboard Leaderboard;

        public PlayableDirector Timeline;
    }
    
    public class LevelManager : MonoBehaviour
    {
        public event System.Action OnLevelStop;

        [SerializeField] private PlayerBehavior[] playerBehaviors;
        [SerializeField] private ChestBatch[] chestBatches;
        [SerializeField] private LoginScreen loginScreen;
        [SerializeField] private Leaderboard leaderboard;
        [SerializeField] private PlayableDirector timeline;

        private LevelContext levelContext;

        private ScoreEntry scoreEntry;
        
#region Lifecycle Events

        private void Awake()
        {
            levelContext = new LevelContext
            {
                LoginScreen = loginScreen,
                
                PlayerBehaviors = playerBehaviors,
                ChestBatches = chestBatches,
                
                Leaderboard = leaderboard,
                
                Timeline = timeline
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
        }

#endregion

#region Methods

        public void AddPlayer(int index)
        {
            if (index >= 0)
            {
                timeline.Play();
            }
        }

        public void Begin()
        {
            levelContext.LoginScreen.enabled = false;
            
            EventBus<LevelEvents.Start>.Raise(LevelEvents.Start.Default);
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