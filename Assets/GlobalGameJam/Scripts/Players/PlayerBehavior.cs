using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Players
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerBehavior : MonoBehaviour, IBindable
    {
        private PlayerInput playerInput;

        private PlayerMovement movement;

        private InputAction moveAction;

#region Lifecycle Events

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
        }

#endregion

#region IBindable Implementation
        
        /// <inheritdoc />
        public void Bind(int playerNumber)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);

            playerInput.SwitchCurrentActionMap("Player");

            moveAction = playerInput.currentActionMap.FindAction("Move");
            moveAction.started += MoveHandler;
            moveAction.performed += MoveHandler;
            moveAction.canceled += MoveHandler;
        }

        /// <inheritdoc />
        public void Release()
        {
            if (moveAction is not null)
            {
                moveAction.started -= MoveHandler;
                moveAction.performed -= MoveHandler;
                moveAction.canceled -= MoveHandler;
            }

            moveAction = null;
        }

#endregion

#region Action Handlers

        private void MoveHandler(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            var direction = Vector3.forward * inputValue.y + Vector3.right * inputValue.x;
            
            movement.SetDirection(direction);
        }

#endregion
    }
}