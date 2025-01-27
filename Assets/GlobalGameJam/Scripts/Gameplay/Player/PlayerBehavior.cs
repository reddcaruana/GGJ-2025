using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehavior : MonoBehaviour, IBindable
    {
        private static readonly int AnimatorDirectionYFloat = Animator.StringToHash("DirectionY");
        private static readonly int AnimatorIsMovingBool = Animator.StringToHash("IsMoving");
        private static readonly int AnimatorIsCarryingObjectBool = Animator.StringToHash("IsCarryingObject");

        [Header("Rendering")]
        [SerializeField] private PlayerRenderer playerRenderer;
        
        [Header("Movement")]
        [SerializeField] private float speed = 7.5f;
        
        [Header("Interaction")]
        [SerializeField] private Transform interactionAnchor;
        [SerializeField] private float interactionDistance = 1.0f;
        [SerializeField] private float interactionRadius = 0.5f;
        [SerializeField] private LayerMask interactionLayer;

        [Header("Bag")]
        [SerializeField] private Transform bagAnchor;
        [SerializeField] private SpriteRenderer bagSpriteRenderer;

        [Header("Throwing")]
        [SerializeField] private float throwSpeed = 10f;
        [SerializeField] private float throwAngle = 45f;

        [Header("Add-Ons")]
        [SerializeField] private Transform throwDirection;

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
                Bag = new Bag(bagAnchor, bagSpriteRenderer),
                Throw = new Throw(throwSpeed, throwAngle)
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
            var dotProducts = new Dictionary<Direction, float>();
            var directions = System.Enum.GetValues(typeof(Direction)).Cast<Direction>();
            
            foreach (var direction in directions)
            {
                dotProducts.Add(direction, Vector3.Dot(inputValue, direction.ToVector()));
            }

            var maxDot = -1f;
            var selection = Direction.East;

            foreach (var dotProduct in dotProducts)
            {
                if (dotProduct.Value > maxDot)
                {
                    maxDot = dotProduct.Value;
                    selection = dotProduct.Key;
                }
            }

            return selection;
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
            var bagWasFull = playerContext.Bag.IsFull;
            playerContext.Interaction.Interact(playerContext);
            if (bagWasFull == false)
            {
                playerRenderer.Animator.SetBool(AnimatorIsCarryingObjectBool, playerContext.Bag.IsFull);
                return;
            }

            if (playerContext.Bag.IsFull)
            {
                playerContext.Throw.Drop(playerContext.Bag, facingDirection);
            }
            
            playerRenderer.Animator.SetBool(AnimatorIsCarryingObjectBool, playerContext.Bag.IsFull);
        }

        private void MoveHandler(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            inputValue = Vector3.forward * value.y + Vector3.right * value.x;

            if (inputValue.sqrMagnitude > Mathf.Epsilon)
            {
                facingDirection = GetDirection(inputValue);
                var interaction = playerContext.Interaction;
                interaction.Direction = facingDirection;
                playerContext.Interaction = interaction;
                
                throwDirection.rotation = Quaternion.LookRotation(facingDirection.ToVector());
            }

            playerRenderer.Animator.SetBool(AnimatorIsMovingBool, inputValue.sqrMagnitude > Mathf.Epsilon);

            if (Mathf.Abs(value.y) > Mathf.Epsilon)
            {
                playerRenderer.Animator.SetFloat(AnimatorDirectionYFloat, value.y);
            }

            if (Mathf.Abs(value.x) > Mathf.Epsilon)
            {
                foreach (var spriteRenderer in playerRenderer.SpriteRenderers)
                {
                    spriteRenderer.flipX = value.x < 0;
                }
            }
        }

#endregion
    }
}