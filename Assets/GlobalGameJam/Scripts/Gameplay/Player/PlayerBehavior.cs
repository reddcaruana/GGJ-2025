using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehavior : MonoBehaviour, IBindable
    {
        [Header("Movement")]
        [SerializeField] private float speed = 7.5f;
        
        [Header("Interaction")]
        [SerializeField] private Transform interactionAnchor;
        [SerializeField] private float interactionDistance = 1.0f;
        [SerializeField] private float interactionRadius = 0.5f;
        [SerializeField] private LayerMask interactionLayer;

        [Header("Bag")]
        [SerializeField] private SpriteRenderer bagSpriteRenderer;

        private PlayerContext playerContext;
        
        private PlayerInput playerInput;

        private InputAction moveAction;
        private InputAction interactionAction;

        private Vector3 inputValue;
        private Direction facingDirection;

#region Lifecycle Events

        private void Awake()
        {
            var attachedRigidbody = GetComponent<Rigidbody>();

            playerContext = new PlayerContext
            {
                Movement = new Movement(speed, attachedRigidbody),
                Interaction = new Interaction(interactionAnchor, interactionDistance, interactionRadius, interactionLayer),
                Bag = new Bag(bagSpriteRenderer),
                Throw = new Throw()
            };
        }

        private void FixedUpdate()
        {
            playerContext.Movement.Move(inputValue);
        }

        private void OnDestroy()
        {
            Release();
        }

#endregion

#region Methods

        public Direction GetDirection(Vector3 vector)
        {
            var dotProducts = new Dictionary<Direction, float>
            {
                { Direction.Forward, Vector3.Dot(inputValue, Vector3.forward) },
                { Direction.Back, Vector3.Dot(inputValue, Vector3.back) },
                { Direction.Left, Vector3.Dot(inputValue, Vector3.left) },
                { Direction.Right, Vector3.Dot(inputValue, Vector3.right) }
            };

            var maxDot = -1f;
            var direction = Direction.Right;

            foreach (var dotProduct in dotProducts)
            {
                if (dotProduct.Value > maxDot)
                {
                    maxDot = dotProduct.Value;
                    direction = dotProduct.Key;
                }
            }

            return direction;
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

            interactionAction = playerInput.currentActionMap.FindAction("Interact");
            interactionAction.started += InteractionHandler;
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

            if (interactionAction is not null)
            {
                interactionAction.started -= InteractionHandler;
            }
            interactionAction = null;

            playerInput = null;
        }

#endregion

#region Action Handlers

        private void InteractionHandler(InputAction.CallbackContext context)
        {
            if (playerContext.Bag.IsFull)
            {
                playerContext.Throw.Drop(playerContext.Bag, facingDirection);
                return;
            }
            
            playerContext.Interaction.Interact(playerContext);
        }

        private void MoveHandler(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            inputValue = Vector3.forward * value.y + Vector3.right * value.x;
            facingDirection = GetDirection(inputValue);

            if (inputValue.sqrMagnitude > Mathf.Epsilon)
            {
                var interaction = playerContext.Interaction;
                interaction.Direction = facingDirection;
                playerContext.Interaction = interaction;
            }
        }

#endregion
    }
}