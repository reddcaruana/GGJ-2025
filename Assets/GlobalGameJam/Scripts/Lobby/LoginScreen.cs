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

        /// <summary>
        /// The event binding for handling player joined events.
        /// </summary>
        private EventBinding<PlayerEvents.Joined> onPlayerJoinedEventBinding;

        /// <summary>
        /// The event binding for handling player left events.
        /// </summary>
        private EventBinding<PlayerEvents.Left> onPlayerLeftEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            onPlayerJoinedEventBinding = new EventBinding<PlayerEvents.Joined>(OnPlayerJoinedEventHandler);
            onPlayerLeftEventBinding = new EventBinding<PlayerEvents.Left>(OnPlayerLeftEventHandler);
        }

        /// <summary>
        /// Registers event bindings when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<PlayerEvents.Joined>.Register(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Left>.Register(onPlayerLeftEventBinding);
        }

        /// <summary>
        /// Deregisters event bindings when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<PlayerEvents.Joined>.Deregister(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Left>.Deregister(onPlayerLeftEventBinding);
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        private void Start()
        {
            var profileRegistry = Singleton.GetOrCreateScriptableObject<ProfileRegistry>();
            var profiles = profileRegistry.Profiles;

            for (var i = 0; i < playerAccounts.Length; i++)
            {
                var account = playerAccounts[i];
                account.Setup(profiles[i]);
                account.enabled = false;
            }
        }

        /// <summary>
        /// Resets the player accounts array.
        /// </summary>
        private void Reset()
        {
            playerAccounts = GetComponentsInChildren<PlayerAccount>();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when a player leaves.
        /// </summary>
        /// <param name="event">The player left event.</param>
        private void OnPlayerLeftEventHandler(PlayerEvents.Left @event)
        {
            var account = playerAccounts[@event.PlayerID];

            account.Release();
            account.enabled = false;
        }

        /// <summary>
        /// Handles the event when a player joins.
        /// </summary>
        /// <param name="event">The player joined event.</param>
        private void OnPlayerJoinedEventHandler(PlayerEvents.Joined @event)
        {
            var account = playerAccounts[@event.PlayerID];

            account.enabled = true;
            account.Bind(@event.PlayerID);
        }

#endregion
    }
}