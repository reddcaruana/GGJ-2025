using System.Collections.Generic;
using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField] private PlayerAccount playerAccountPrefab;
        [SerializeField] private Transform playerAccountParent;

        private PlayerDataManager playerDataManager;
        private readonly Dictionary<int, PlayerAccount> playerAccounts = new();

#region Lifecycle Events

        private void Awake()
        {
            playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
        }

        private void OnEnable()
        {
            Activate();
        }

        private void OnDisable()
        {
            Deactivate();
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
            var account = Instantiate(playerAccountPrefab, playerAccountParent);
            account.name = $"PlayerAccount_{playerNumber}";
            
            playerAccounts.Add(playerNumber, account);
        }

        public void OnPlayerLeftHandler(int playerNumber)
        {
            if (playerAccounts.TryGetValue(playerNumber, out var account) == false)
            {
                return;
            }
            
            Destroy(account.gameObject);
            playerAccounts.Remove(playerNumber);
        }

#endregion
    }
}