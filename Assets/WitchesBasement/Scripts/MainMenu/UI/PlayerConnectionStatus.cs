using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.MainMenu
{
    public class PlayerConnectionStatus : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerMap;
        [SerializeField] private List<GameObject> playerDisplays = new();
        
#region Lifecycle Events

        private void OnEnable()
        {
            playerMap.OnItemAdded += OnItemAdded;
            playerMap.OnItemRemoved += OnItemRemoved;
        }

        private void OnDisable()
        {
            playerMap.OnItemAdded -= OnItemAdded;
            playerMap.OnItemRemoved -= OnItemRemoved;
        }

        private void Reset()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                playerDisplays.Add(transform.GetChild(i).gameObject);
                playerDisplays[i].SetActive(false);
            }
        }

#endregion

#region Subscriptions

        private void OnItemAdded(int playerID, PlayerInput playerInput)
        {
            playerDisplays[playerID].SetActive(true);
        }

        private void OnItemRemoved(int playerID, PlayerInput playerInput)
        {
            playerDisplays[playerID].SetActive(false);
        }

#endregion
    }
}