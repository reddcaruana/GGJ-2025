using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using WitchesBasement.Players;

namespace WitchesBasement.MainMenu
{
    internal class MenuWindow : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerMap;
        [SerializeField] private Selectable firstSelection;

#region Lifecycle Events

        private void OnEnable()
        {
            playerMap.OnItemAdded += OnPlayerAdded;
        }

        private void OnDisable()
        {
            playerMap.OnItemAdded -= OnPlayerAdded;
        }

#endregion

#region Subscriptions

        private void OnPlayerAdded(int playerID, PlayerInput playerInput)
        {
            var eventSystem = playerInput.GetComponent<MultiplayerEventSystem>();

            if (playerID != 0)
            {
                eventSystem.enabled = false;
                return;
            }

            eventSystem.playerRoot = gameObject;
            eventSystem.enabled = true;
            eventSystem.SetSelectedGameObject(firstSelection.gameObject);
        }

#endregion
    }
}