using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Players
{
    public class PlayerDataManager : MonoBehaviour
    {
        public event System.Action<int> OnPlayerJoined;
        public event System.Action<int> OnPlayerLeft;
        
        private readonly Dictionary<int, PlayerInput> playerInputMap = new();

        private PlayerInputManager playerInputManager;

#region Lifecycle Events

        private void Awake()
        {
            playerInputManager = Singleton.GetOrCreateMonoBehaviour<PlayerInputManager>();
            for (var i = 0; i < playerInputManager.maxPlayerCount; i++)
            {
                playerInputMap.Add(i, null);
            }
        }

        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnPlayerJoinedHandler;
            playerInputManager.onPlayerLeft += OnPlayerLeftHandler;
        }

        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoinedHandler;
            playerInputManager.onPlayerLeft -= OnPlayerLeftHandler;
        }

        private void OnGUI()
        {
            var players = GetActivePlayers();
            var rect = new Rect(10, 10, 100, 50);

            foreach (var player in players)
            {
                GUI.Label(rect, player.ToString());
                rect.y += rect.height;
            }
        }

#endregion

#region Methods

        public int[] GetActivePlayers()
        {
            return playerInputMap.Keys.ToArray();
        }

        private int GetFirstAvailableIndex()
        {
            foreach (var map in playerInputMap)
            {
                if (map.Value is null)
                {
                    return map.Key;
                }
            }

            return -1;
        }

        public PlayerInput GetPlayerInput(int playerNumber)
        {
            return playerInputMap.GetValueOrDefault(playerNumber, null);
        }
        
        public void Leave(int playerNumber)
        {
            if (playerInputMap.TryGetValue(playerNumber, out var playerInput) == false)
            {
                Debug.LogWarning($"Player Number {playerNumber} was not registered.");
                return;
            }
            
            Destroy(playerInput.gameObject);
        }

#endregion

#region Event Handlers

        private void OnPlayerJoinedHandler(PlayerInput playerInput)
        {
            var index = GetFirstAvailableIndex();
            if (index == -1)
            {
                Debug.LogWarning("All player slots are occupied. Will not register this player.");
                return;
            }

            playerInputMap[index] = playerInput;

            var playerInputObject = playerInput.gameObject;
            playerInputObject.name = $"PlayerInput_{index}";
            playerInputObject.transform.SetParent(transform);
            
            OnPlayerJoined?.Invoke(index);
        }

        private void OnPlayerLeftHandler(PlayerInput playerInput)
        {
            foreach (var map in playerInputMap)
            {
                if (map.Value == playerInput)
                {
                    playerInputMap[map.Key] = null;
                    OnPlayerLeft?.Invoke(map.Key);
                    
                    playerInputManager.EnableJoining();
                    return;
                }
            }
            
            Debug.LogWarning($"Player {playerInput.playerIndex} was not registered and will not be removed.");
        }

#endregion
    }
}