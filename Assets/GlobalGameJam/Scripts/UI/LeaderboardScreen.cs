using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField] private LeaderboardAccount[] playerAccounts;

        private EventBinding<LevelEvents.Leaderboard> onLeaderboardEventBinding;

        private EventBinding<ScoreEvents.SetInitial> onSetInitialEventBinding;

        private int initialsSubmitted;
        
#region Lifecycle Events

        private void Awake()
        {
            onLeaderboardEventBinding = new EventBinding<LevelEvents.Leaderboard>(OnLeaderboardEventHandler);
            onSetInitialEventBinding = new EventBinding<ScoreEvents.SetInitial>(OnSetInitialEventHandler);
        }

        private void OnEnable()
        {
            EventBus<LevelEvents.Leaderboard>.Register(onLeaderboardEventBinding);
            EventBus<ScoreEvents.SetInitial>.Register(onSetInitialEventBinding);
        }

        private void OnDisable()
        {
            EventBus<LevelEvents.Leaderboard>.Deregister(onLeaderboardEventBinding);
            EventBus<ScoreEvents.SetInitial>.Deregister(onSetInitialEventBinding);
        }

        private void Reset()
        {
            playerAccounts = GetComponentsInChildren<LeaderboardAccount>();
        }

        private void Start()
        {
            var profileRegistry = Singleton.GetOrCreateScriptableObject<ProfileRegistry>();
            var profiles = profileRegistry.Profiles;

            for (var i = 0; i < profiles.Length; i++)
            {
                var account = playerAccounts[i];
                account.Setup(profiles[i]);
            }
        }
        
#endregion

#region Event Handlers

        private void OnLeaderboardEventHandler(LevelEvents.Leaderboard @event)
        {
            for (var i = 0; i < playerAccounts.Length; i++)
            {
                playerAccounts[i].Bind(i);
            }
        }
        
        private void OnSetInitialEventHandler(ScoreEvents.SetInitial @event)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            
            initialsSubmitted++;
            if (initialsSubmitted < playerDataManager.GetActivePlayers().Length)
            {
                return;
            }

            initialsSubmitted = 0;
            Debug.Log("Complete.");
        }
        
#endregion
    }
}