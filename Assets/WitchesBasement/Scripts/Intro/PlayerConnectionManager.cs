using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.Intro
{
    internal class PlayerConnectionManager : MonoBehaviour
    {
        [SerializeField] private PlayerConnection playerConnection;
        [SerializeField] private ScriptableDictionaryPlayerID playerDictionary;

        private readonly Dictionary<int, PlayerConnection> playerConnections = new();
        
#region Lifecycle Events

        private void OnEnable()
        {
            playerDictionary.OnItemAdded += OnItemAdded;
            playerDictionary.OnItemRemoved += OnItemRemoved;
        }

        private void OnDisable()
        {
            playerDictionary.OnItemAdded -= OnItemAdded;
            playerDictionary.OnItemRemoved -= OnItemRemoved;
        }

#endregion

#region Subscriptions
        
        private void OnItemAdded(int playerID, PlayerInput playerInput)
        {
            if (playerID == 0)
            {
                return;
            }
            
            var newConnection = Instantiate(playerConnection, transform);
            newConnection.name = $"Connection_{playerID:0}";
            newConnection.Bind(playerInput);

            playerConnections[playerID] = newConnection;
        }

        private void OnItemRemoved(int playerID, PlayerInput playerInput)
        {
            if (playerConnections.TryGetValue(playerID, out var connection))
            {
                Destroy(connection.gameObject);
                playerConnections.Remove(playerID);
            }
        }
        
#endregion
    }
}