using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.MainMenu
{
    internal class SplashScreen : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerMap;

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
            gameObject.SetActive(false);
        }

#endregion
    }
}