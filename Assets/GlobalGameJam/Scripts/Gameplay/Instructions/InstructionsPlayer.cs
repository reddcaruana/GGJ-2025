using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Class responsible for handling instructions player input.
    /// </summary>
    public class InstructionsPlayer : MonoBehaviour, IBindable
    {
        /// <summary>
        /// The PlayerInput component associated with this player.
        /// </summary>
        private PlayerInput playerInput;

        /// <summary>
        /// The input action for confirming instructions.
        /// </summary>
        private InputAction confirmAction;

#region Implementation of IBindable

        /// <summary>
        /// Binds the player input for the specified player number.
        /// </summary>
        /// <param name="playerNumber">The player number to bind input for.</param>
        public void Bind(int playerNumber)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);

            if (playerInput is null)
            {
                return;
            }

            playerInput.SwitchCurrentActionMap("Instructions");

            confirmAction = playerInput.currentActionMap.FindAction("Confirm");
            confirmAction.started += ConfirmHandler;
        }

        /// <summary>
        /// Releases the player input bindings.
        /// </summary>
        public void Release()
        {
            if (confirmAction is not null)
            {
                confirmAction.started -= ConfirmHandler;
            }
            confirmAction = null;

            playerInput = null;
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the confirm action input.
        /// Raises the Resume event when the confirm action is triggered.
        /// </summary>
        /// <param name="context">The context of the input action.</param>
        private void ConfirmHandler(InputAction.CallbackContext context)
        {
            EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
        }

#endregion
    }
}