using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Events;
using WitchesBasement.Players;

namespace WitchesBasement.Onboarding
{
    internal class Lobby : EventBoundMonoBehaviour
    {
        [SerializeField] private LobbyPlayer[] players;

#region Lifecycle Events

        private void Reset()
        {
            players = GetComponentsInChildren<LobbyPlayer>();
        }

        private void Start()
        {
            var registry = Singleton.GetOrCreateScriptableObject<ProfileRegistry>();
            var profiles = registry.Profiles;

            for (var i = 0; i < players.Length; i++)
            {
                var player = players[i];
                var profile = profiles[i];
                
                player.Setup(profile);
                player.enabled = false;
            }
        }

#endregion

#region Overrides of EventBindingBase

        /// <inheritdoc />
        protected override void InitializeEventBindings()
        {
            RegisterEvent<PlayerStatusChangedEvent>(PlayerStatusChangedHandler);
        }

#endregion

#region Event Handlers

        private void PlayerStatusChangedHandler(PlayerStatusChangedEvent @event)
        {
            var player = players[@event.PlayerID];
            
            switch (@event.Status)
            {
                case PlayerStatusChangedEvent.StatusType.Joined:
                    player.enabled = true;
                    player.Binding.Bind(@event.PlayerID);
                    break;
                
                case PlayerStatusChangedEvent.StatusType.Left:
                    player.Binding.Release();
                    player.enabled = false;
                    break;
            }
        }

#endregion
    }
}