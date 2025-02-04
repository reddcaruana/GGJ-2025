using GlobalGameJam.Events;
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
        /// The input action for moving to the next instructions.
        /// </summary>
        private InputAction nextAction;

        /// <summary>
        /// The input action for returning to the previous instruction.
        /// </summary>
        private InputAction backAction;

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

            nextAction = playerInput.currentActionMap.FindAction("Next");
            nextAction.started += NextHandler;

            backAction = playerInput.currentActionMap.FindAction("Back");
            backAction.started += BackHandler;
        }

        /// <summary>
        /// Releases the player input bindings.
        /// </summary>
        public void Release()
        {
            if (nextAction is not null)
            {
                nextAction.started -= NextHandler;
            }
            nextAction = null;

            if (backAction is not null)
            {
                backAction.started -= BackHandler;
            }
            backAction = null;

            playerInput = null;
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the input action for going back to the previous instruction.
        /// </summary>
        /// <param name="context">The context of the input action.</param>
        private static void BackHandler(InputAction.CallbackContext context)
        {
            EventBus<InstructionsEvent.Navigate>.Raise(new InstructionsEvent.Navigate
            {
                Navigation = NavigationMode.Previous
            });
        }

        /// <summary>
        /// Handles the input action for moving to the next instruction.
        /// </summary>
        /// <param name="context">The context of the input action.</param>
        private static void NextHandler(InputAction.CallbackContext context)
        {
            EventBus<InstructionsEvent.Navigate>.Raise(new InstructionsEvent.Navigate
            {
                Navigation = NavigationMode.Next
            });
        }

#endregion
    }
}