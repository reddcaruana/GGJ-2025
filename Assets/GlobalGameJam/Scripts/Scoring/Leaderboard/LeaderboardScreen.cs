using GlobalGameJam.Data;
using GlobalGameJam.Events;
using GlobalGameJam.Gameplay;
using GlobalGameJam.Players;
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
        private EventBinding<LevelEvents.SetMode> onSetLevelModeEventBinding;

        /// <summary>
        /// Event binding for the ScoreEvents.SetInitial event.
        /// </summary>
        private EventBinding<ScoreEvents.SetInitial> onSetInitialEventBinding;

        /// <summary>
        /// Event binding for the PlayerEvents.Joined event.
        /// </summary>
        private EventBinding<PlayerEvents.Joined> onPlayerJoinedEventBinding;

        /// <summary>
        /// Event binding for the PlayerEvents.Left event.
        /// </summary>
        private EventBinding<PlayerEvents.Left> onPlayerLeftEventBinding;

        /// <summary>
        /// Counter for the number of initials submitted by players.
        /// </summary>
        private int initialsSubmitted;

#region Lifecycle Events

        /// <summary>
        /// </summary>
        private void Awake()
        {
            onSetLevelModeEventBinding = new EventBinding<LevelEvents.SetMode>(OnSetLevelModeEventHandler);
            onSetInitialEventBinding = new EventBinding<ScoreEvents.SetInitial>(OnSetInitialEventHandler);

            onPlayerJoinedEventBinding = new EventBinding<PlayerEvents.Joined>(OnPlayerJoinedEventHandler);
            onPlayerLeftEventBinding = new EventBinding<PlayerEvents.Left>(OnPlayerLeftEventHandler);
        }

        /// <summary>
        /// Registers the event bindings for the leaderboard and score events.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.SetMode>.Register(onSetLevelModeEventBinding);
            EventBus<ScoreEvents.SetInitial>.Register(onSetInitialEventBinding);
            
            EventBus<PlayerEvents.Joined>.Register(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Left>.Register(onPlayerLeftEventBinding);
        }

        /// <summary>
        /// Deregisters the event bindings for the leaderboard and score events.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.SetMode>.Deregister(onSetLevelModeEventBinding);
            EventBus<ScoreEvents.SetInitial>.Deregister(onSetInitialEventBinding);
            
            EventBus<PlayerEvents.Joined>.Deregister(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Left>.Deregister(onPlayerLeftEventBinding);
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
                account.gameObject.SetActive(false);
            }
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the LevelEvents.Leaderboard event.
        /// Binds the player accounts for each player.
        /// </summary>
        /// <param name="event">The LevelEvents.Leaderboard event.</param>
        private void OnSetLevelModeEventHandler(LevelEvents.SetMode @event)
        {
            if (@event.Mode is not LevelMode.Leaderboard)
            {
                return;
            }
            
            for (var i = 0; i < playerAccounts.Length; i++)
            {
                playerAccounts[i].Bind(i);
            }
        }

        /// <summary>
        /// Handles the Player joined event.
        /// </summary>
        /// <param name="event">The PlayerEvents.Joined event.</param>
        private void OnPlayerJoinedEventHandler(PlayerEvents.Joined @event)
        {
            var playerID = @event.PlayerID;
            playerAccounts[playerID].gameObject.SetActive(true);
        }

        /// <summary>
        /// Handles the Player left event.
        /// </summary>
        /// <param name="event">The PlayerEvents.Left event.</param>
        private void OnPlayerLeftEventHandler(PlayerEvents.Left @event)
        {
            var playerID = @event.PlayerID;
            playerAccounts[playerID].gameObject.SetActive(false);
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