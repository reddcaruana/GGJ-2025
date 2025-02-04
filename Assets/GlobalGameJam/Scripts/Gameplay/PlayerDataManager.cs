using System.Collections.Generic;
using System.Linq;
using GlobalGameJam.Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages player data and handles player join/leave events.
    /// </summary>
    public class PlayerDataManager : MonoBehaviour
    {
        /// <summary>
        /// A dictionary mapping player indices to their PlayerInput instances.
        /// </summary>
        private readonly Dictionary<int, PlayerInput> playerInputMap = new();

        /// <summary>
        /// The player input manager responsible for handling player input.
        /// </summary>
        private PlayerInputManager playerInputManager;

        /// <summary>
        /// The number of registered players.
        /// </summary>
        public int Count => playerInputMap.Count;

        private EventBinding<PlayerEvents.EnableJoining> onEnableJoiningEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the player input manager and player input map.
        /// </summary>
        private void Awake()
        {
            playerInputManager = Singleton.GetOrCreateMonoBehaviour<PlayerInputManager>();
            for (var i = 0; i < playerInputManager.maxPlayerCount; i++)
            {
                playerInputMap.Add(i, null);
            }
            
            playerInputManager.DisableJoining();

            onEnableJoiningEventBinding = new EventBinding<PlayerEvents.EnableJoining>(OnEnableJoiningEventHandler);
        }

        /// <summary>
        /// Subscribes to player join and leave events.
        /// </summary>
        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnPlayerJoinedHandler;
            playerInputManager.onPlayerLeft += OnPlayerLeftHandler;
            
            EventBus<PlayerEvents.EnableJoining>.Register(onEnableJoiningEventBinding);
        }

        /// <summary>
        /// Unsubscribes from player join and leave events.
        /// </summary>
        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoinedHandler;
            playerInputManager.onPlayerLeft -= OnPlayerLeftHandler;
            
            EventBus<PlayerEvents.EnableJoining>.Deregister(onEnableJoiningEventBinding);

        }

#if GAME_DEBUG
        /// <summary>
        /// Displays the active players on the GUI.
        /// </summary>
        private void OnGUI()
        {
            var rect = new Rect(10, 10, 100, 50);
            GUI.Label(rect, $"Player count: {GetActivePlayers().Length}");
            rect.y += rect.height;

            foreach (var map in playerInputMap)
            {
                if (map.Value is null)
                {
                    continue;
                }
                
                GUI.Label(rect, map.Key.ToString());
                rect.y += rect.height;
            }
        }
#endif

#endregion

#region Methods

        /// <summary>
        /// Gets the active players.
        /// </summary>
        /// <returns>An array of active player indices.</returns>
        public int[] GetActivePlayers()
        {
            return playerInputMap.Keys
                .Where(id => playerInputMap[id] is not null)
                .ToArray();
        }

        /// <summary>
        /// Gets the first available player index.
        /// </summary>
        /// <returns>The first available player index, or -1 if none are available.</returns>
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

        /// <summary>
        /// Gets the PlayerInput for a given player number.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>The PlayerInput for the given player number, or null if not found.</returns>
        public PlayerInput GetPlayerInput(int playerNumber)
        {
            return playerInputMap.GetValueOrDefault(playerNumber, null);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Enables player joining.
        /// </summary>
        private void OnEnableJoiningEventHandler()
        {
            Debug.Log("Joining");
            playerInputManager.EnableJoining();
        }
        
        /// <summary>
        /// Handles the event when a player joins.
        /// </summary>
        /// <param name="playerInput">The PlayerInput of the joined player.</param>
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

            EventBus<PlayerEvents.Joined>.Raise(new PlayerEvents.Joined
            {
                PlayerID = index,
                PlayerInput = playerInput
            });
        }

        /// <summary>
        /// Handles the event when a player leaves.
        /// </summary>
        /// <param name="playerInput">The PlayerInput of the left player.</param>
        private void OnPlayerLeftHandler(PlayerInput playerInput)
        {
            foreach (var map in playerInputMap)
            {
                if (map.Value == playerInput)
                {
                    playerInputMap[map.Key] = null;

                    EventBus<PlayerEvents.Left>.Raise(new PlayerEvents.Left
                    {
                        PlayerID = map.Key,
                        PlayerInput = playerInput
                    });
                    
                    playerInputManager.EnableJoining();
                    return;
                }
            }

            Debug.LogWarning($"Player {playerInput.playerIndex} was not registered and will not be removed.");
        }

#endregion
    }
}