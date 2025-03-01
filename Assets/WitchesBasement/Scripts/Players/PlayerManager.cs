using UnityEngine;
using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerMap;
        
        private PlayerInputManager inputManager;

#region Methods

        private void Awake()
        {
            inputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            inputManager.onPlayerJoined += OnPlayerJoinedHandler;
            inputManager.onPlayerLeft += OnPlayerLeftHandler;
        }

        private void OnDisable()
        {
            inputManager.onPlayerJoined -= OnPlayerJoinedHandler;
            inputManager.onPlayerLeft -= OnPlayerLeftHandler;
        }

        private void Reset()
        {
            inputManager = GetComponent<PlayerInputManager>();
            inputManager.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        }

        private void Start()
        {
            if (inputManager.playerCount == 0)
            {
                inputManager.JoinPlayer();
            }
        }

#endregion

#region Methods

        private int GetFirstAvailableIndex()
        {
            for (var i = 0; i < inputManager.maxPlayerCount; i++)
            {
                if (playerMap.ContainsKey(i) == false)
                {
                    return i;
                }
            }

            return -1;
        }

#endregion

#region Subscriptions

        private void OnPlayerJoinedHandler(PlayerInput playerInput)
        {
            var index = GetFirstAvailableIndex();
            if (index == -1)
            {
                Debug.LogWarning("All player slots are occupied. Will not register this player.");
                return;
            }
            
            playerMap.Add(index, playerInput);
            
            var playerInputObject = playerInput.gameObject;
            playerInputObject.name = $"PlayerInput_{index}";
            playerInputObject.transform.SetParent(transform);
        }

        private void OnPlayerLeftHandler(PlayerInput playerInput)
        {
            foreach (var item in playerMap)
            {
                if (item.Value == playerInput)
                {
                    playerMap.Remove(item.Key);
                    
                    inputManager.EnableJoining();
                    return;
                }
            }
            
            Debug.LogWarning($"Player {playerInput.playerIndex} was not registered and will not be removed.");
        }

#endregion
    }
}