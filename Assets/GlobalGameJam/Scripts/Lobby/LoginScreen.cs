using System.Linq;
using GlobalGameJam.Data;
using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam.Lobby
{
    /// <summary>
    /// Represents the login screen for the game UI.
    /// </summary>
    public class LoginScreen : MonoBehaviour
    {
        /// <summary>
        /// Array of player accounts.
        /// </summary>
        [SerializeField] private PlayerAccount[] playerAccounts;

        private PlayerDataManager playerDataManager;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            var profileRegistry = Singleton.GetOrCreateScriptableObject<ProfileRegistry>();
            var profiles = profileRegistry.Profiles.OrderBy(_ => Random.value).ToArray();

            for (var i = 0; i < playerAccounts.Length; i++)
            {
                var account = playerAccounts[i];
                account.Setup(profiles[i]);
                account.enabled = false;
            }

            Activate();
        }

        /// <summary>
        /// Called when the object becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            Deactivate();
        }

        /// <summary>
        /// Resets the player accounts array.
        /// </summary>
        private void Reset()
        {
            playerAccounts = GetComponentsInChildren<PlayerAccount>();
        }

#endregion

#region Methods

        /// <summary>
        /// Activates the login screen by subscribing to player events.
        /// </summary>
        public void Activate()
        {
            playerDataManager.OnPlayerJoined += OnPlayerJoinedHandler;
            playerDataManager.OnPlayerLeft += OnPlayerLeftHandler;
        }

        /// <summary>
        /// Deactivates the login screen by unsubscribing from player events.
        /// </summary>
        public void Deactivate()
        {
            playerDataManager.OnPlayerJoined -= OnPlayerJoinedHandler;
            playerDataManager.OnPlayerLeft -= OnPlayerLeftHandler;
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when a player joins.
        /// </summary>
        /// <param name="playerNumber">The number of the player who joined.</param>
        private void OnPlayerJoinedHandler(int playerNumber)
        {
            var account = playerAccounts[playerNumber];

            account.enabled = true;
            account.Bind(playerNumber);
        }

        /// <summary>
        /// Handles the event when a player leaves.
        /// </summary>
        /// <param name="playerNumber">The number of the player who left.</param>
        private void OnPlayerLeftHandler(int playerNumber)
        {
            var account = playerAccounts[playerNumber];
            account.Release();
            account.enabled = false;
        }

#endregion
    }
}