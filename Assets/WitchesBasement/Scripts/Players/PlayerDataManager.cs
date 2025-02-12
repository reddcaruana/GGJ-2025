using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Events;

namespace WitchesBasement.Players
{
    public class PlayerDataManager : MonoBehaviour
    {
        private readonly Dictionary<int, PlayerInput> playerInputMap = new();

        private PlayerInputManager playerInputManager;

        public int ActivePlayerCount => playerInputMap.Count(kvp => kvp.Value is not null);

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

#endregion

#region Methods

        public int[] GetActivePlayerIDs()
        {
            return playerInputMap.Keys
                .Where(id => playerInputMap[id] is not null)
                .ToArray();
        }

        private int GetFirstNullIndex()
        {
            for (var i = 0; i < playerInputMap.Count; i++)
            {
                if (playerInputMap[i] is null)
                {
                    return i;
                }
            }

            return -1;
        }

        public PlayerInput FindInputByID(int playerID)
        {
            return playerInputMap.GetValueOrDefault(playerID, null);
        }

#endregion

#region Event Handlers

        private void OnPlayerJoinedHandler(PlayerInput playerInput)
        {
            var id = GetFirstNullIndex();
            if (id == -1)
            {
                Debug.LogWarning("All player slots are occupied. Will not register this player.");
                return;
            }

            playerInputMap[id] = playerInput;

            var inputGameObject = playerInput.gameObject;
            inputGameObject.name = $"PlayerInput_{id:00}";
            inputGameObject.transform.SetParent(transform);
            
            EventBus<PlayerStatusChangedEvent>.Raise(new PlayerStatusChangedEvent
            {
                Status = PlayerStatusChangedEvent.StatusType.Joined,
                PlayerID = id,
                PlayerInput = playerInput
            });
        }

        private void OnPlayerLeftHandler(PlayerInput playerInput)
        {
            for (var i = 0; i < playerInputMap.Count; i++)
            {
                if (playerInputMap[i] != playerInput)
                {
                    continue;
                }

                playerInputMap[i] = null;
                
                EventBus<PlayerStatusChangedEvent>.Raise(new PlayerStatusChangedEvent
                {
                    Status = PlayerStatusChangedEvent.StatusType.Left,
                    PlayerID = i,
                    PlayerInput = playerInput
                });

                return;
            }
            
            Debug.LogWarning($"Player {playerInput.playerIndex} was not registered and will not be removed.");
        }

#endregion
    }
}