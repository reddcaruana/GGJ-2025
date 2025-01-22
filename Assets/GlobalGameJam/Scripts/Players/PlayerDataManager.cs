using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Players
{
    public class PlayerDataManager : MonoBehaviour
    {
        public event System.Action<int> OnPlayerJoined;
        public event System.Action<int> OnPlayerLeft;
        
        private readonly Dictionary<int, PlayerInput> playerInputMap = new();

        private readonly List<int> playerNumberMap = new();

        private PlayerInputManager playerInputManager;

#region Lifecycle Events

        private void Awake()
        {
            playerInputManager = Singleton.GetOrCreateMonoBehaviour<PlayerInputManager>();
            for (var i = 0; i < playerInputManager.maxPlayerCount; i++)
            {
                playerNumberMap.Add(-1);
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
            var playerNumbers = new List<int>();
            for (var i = 0; i < playerNumberMap.Count; i++)
            {
                if (playerNumberMap[i] != -1)
                {
                    playerNumbers.Add(i);
                }
            }

            return playerNumbers.ToArray();
        }

        public PlayerInput GetPlayerInput(int playerNumber)
        {
            var playerIndex = playerNumberMap[playerNumber];
            return playerInputMap.GetValueOrDefault(playerIndex, null);
        }

#endregion

#region Event Handlers

        private void OnPlayerJoinedHandler(PlayerInput playerInput)
        {
            var index = playerInput.playerIndex;
            if (playerInputMap.TryAdd(index, playerInput) == false)
            {
                Debug.LogWarning($"Player {index} is already registered.");
                return;
            }

            var firstUnassignedPlayer = playerNumberMap.FindIndex(playerID => playerID == -1);
            playerNumberMap[firstUnassignedPlayer] = index;

            var playerInputObject = playerInput.gameObject;
            playerInputObject.name = $"PlayerInput_{index}";
            playerInputObject.transform.SetParent(transform);
            
            OnPlayerJoined?.Invoke(firstUnassignedPlayer);
        }

        private void OnPlayerLeftHandler(PlayerInput playerInput)
        {
            var index = playerInput.playerIndex;
            if (playerInputMap.ContainsKey(index) == false)
            {
                Debug.LogWarning($"Player {index} was not registered and will not be removed.");
            }

            playerInputMap.Remove(index);

            var playerNumber = playerNumberMap.FindIndex(playerID => playerID == index);
            if (playerNumber != -1)
            {
                playerNumberMap[playerNumber] = -1;
            }
            
            playerInputManager.EnableJoining();
            
            OnPlayerLeft?.Invoke(playerNumber);
        }

#endregion
    }
}