using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using GlobalGameJam.UI;
using UnityEngine;

namespace GlobalGameJam
{
    /// <summary>
    /// Manages the leaderboard screen, including player accounts and event handling for leaderboard and score events.
    /// </summary>
    public class LeaderboardScreen : MonoBehaviour
    {
        /// <summary>
        /// Array of player accounts displayed on the leaderboard.
        /// </summary>
        [SerializeField] private LeaderboardAccount[] playerAccounts;

        /// <summary>
        /// Event binding for the LevelEvents.Leaderboard event.
        /// </summary>
        private EventBinding<LevelEvents.Leaderboard> onLeaderboardEventBinding;

        /// <summary>
        /// Event binding for the ScoreEvents.SetInitial event.
        /// </summary>
        private EventBinding<ScoreEvents.SetInitial> onSetInitialEventBinding;

        /// <summary>
        /// Counter for the number of initials submitted by players.
        /// </summary>
        private int initialsSubmitted;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event bindings for the leaderboard and score events.
        /// </summary>
        private void Awake()
        {
            onLeaderboardEventBinding = new EventBinding<LevelEvents.Leaderboard>(OnLeaderboardEventHandler);
            onSetInitialEventBinding = new EventBinding<ScoreEvents.SetInitial>(OnSetInitialEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the event bindings for the leaderboard and score events.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.Leaderboard>.Register(onLeaderboardEventBinding);
            EventBus<ScoreEvents.SetInitial>.Register(onSetInitialEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the event bindings for the leaderboard and score events.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.Leaderboard>.Deregister(onLeaderboardEventBinding);
            EventBus<ScoreEvents.SetInitial>.Deregister(onSetInitialEventBinding);
        }

        /// <summary>
        /// Resets the player accounts array to the LeaderboardAccount components found in the children of this GameObject.
        /// </summary>
        private void Reset()
        {
            playerAccounts = GetComponentsInChildren<LeaderboardAccount>();
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Sets up the player accounts with profile data.
        /// </summary>
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

        /// <summary>
        /// Handles the LevelEvents.Leaderboard event.
        /// Binds the player accounts for each player.
        /// </summary>
        /// <param name="event">The LevelEvents.Leaderboard event.</param>
        private void OnLeaderboardEventHandler(LevelEvents.Leaderboard @event)
        {
            for (var i = 0; i < playerAccounts.Length; i++)
            {
                playerAccounts[i].Bind(i);
            }
        }

        /// <summary>
        /// Handles the ScoreEvents.SetInitial event.
        /// Increments the initials submitted counter and logs a message when all initials are submitted.
        /// </summary>
        /// <param name="event">The ScoreEvents.SetInitial event.</param>
        private void OnSetInitialEventHandler(ScoreEvents.SetInitial @event)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();

            initialsSubmitted++;
            if (initialsSubmitted < playerDataManager.GetActivePlayers().Length)
            {
                return;
            }

            initialsSubmitted = 0;

            EventBus<ScoreEvents.Submit>.Raise(ScoreEvents.Submit.Default);
        }

#endregion
    }
}