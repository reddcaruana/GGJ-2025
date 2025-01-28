using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField] private LeaderboardAccount[] playerAccounts;

        private EventBinding<LevelEvents.Leaderboard> onLeaderboardEventBinding;

#region Lifecycle Events

        private void Awake()
        {
            onLeaderboardEventBinding = new EventBinding<LevelEvents.Leaderboard>(OnLeaderboardEventHandler);
        }

        private void OnEnable()
        {
            EventBus<LevelEvents.Leaderboard>.Register(onLeaderboardEventBinding);
        }

        private void OnDisable()
        {
            EventBus<LevelEvents.Leaderboard>.Deregister(onLeaderboardEventBinding);
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

#endregion
    }
}