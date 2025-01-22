using System.Linq;
using GlobalGameJam.Data;
using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam.Lobby
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField] private PlayerAccount[] playerAccounts;

        private PlayerDataManager playerDataManager;

#region Lifecycle Events

        private void Awake()
        {
            playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
        }

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

        private void OnDisable()
        {
            Deactivate();
        }

        private void Reset()
        {
            playerAccounts = GetComponentsInChildren<PlayerAccount>();
        }

#endregion

#region Methods

        public void Activate()
        {
            playerDataManager.OnPlayerJoined += OnPlayerJoinedHandler;
            playerDataManager.OnPlayerLeft += OnPlayerLeftHandler;
        }

        public void Deactivate()
        {
            playerDataManager.OnPlayerJoined -= OnPlayerJoinedHandler;
            playerDataManager.OnPlayerLeft -= OnPlayerLeftHandler;
        }

#endregion

#region Event Handlers

        public void OnPlayerJoinedHandler(int playerNumber)
        {
            var account = playerAccounts[playerNumber];
            
            account.enabled = true;
            account.Bind(playerNumber);
        }

        public void OnPlayerLeftHandler(int playerNumber)
        {
            var account = playerAccounts[playerNumber];
            account.Release();
            account.enabled = false;
        }

#endregion
    }
}