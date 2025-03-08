using Obvious.Soap;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Data;
using WitchesBasement.Players;

namespace WitchesBasement.System
{
    public class PlayerInventory : MonoBehaviour, IBindableComponent
    {
        [Header("Held Object")]
        [SerializeField] private Transform carryAnchor;
        [SerializeField] private FloatVariable throwForce;
        [SerializeField] private FloatVariable throwAngle;
        
        [Header("Interactions")]
        [SerializeField] private Transform interactionSource;
        [SerializeField] private FloatVariable interactionDistance;
        [SerializeField] private FloatVariable interactionRadius;
        [SerializeField] private LayerMask interactionLayers;

        [Header("References")]
        [SerializeField] private SpriteRenderer inventorySpriteRenderer;
        
        private ItemData currentData;
        
        private InputAction interactAction;
        private InputAction moveAction;

        private Vector3 direction = Vector3.down;
        
#region Implementation of IBindableComponent

        /// <inheritdoc />
        public void Bind(PlayerInput playerInput)
        {
            var currentActionMap = playerInput.currentActionMap;
            
            interactAction = currentActionMap.FindAction("Interact");
            interactAction.started += OnInteract;

            moveAction = currentActionMap.FindAction("Move");
            moveAction.started += OnMove;
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }

        /// <inheritdoc />
        public void Release()
        {
            if (interactAction is not null)
            {
                interactAction.started -= OnInteract;
                interactAction = null;
            }

            if (moveAction is not null)
            {
                moveAction.started += OnMove;
                moveAction.performed += OnMove;
                moveAction.canceled += OnMove;
                moveAction = null;
            }
        }

#endregion

#region Methods

        private void Interact()
        {
            var point = interactionSource.position + direction * interactionDistance.Value;

            var results = new Collider[4];
            var numCollisions = Physics.OverlapSphereNonAlloc(point, interactionRadius.Value, results, interactionLayers.value);

            if (numCollisions == 0)
            {
                return;
            }
            
            float closestDistance = float.MaxValue;
            Collider closestCollider = results[0];

            for (var i = 0; i < numCollisions; i++)
            {
                var result = results[i];
                var resultDistance = Vector3.Distance(result.transform.position, point);

                if (resultDistance < closestDistance && result.GetComponent<IUsable>() != null)
                {
                    closestDistance = resultDistance;
                    closestCollider = result;
                }
            }

            var usable = closestCollider.GetComponent<IUsable>();
            var itemData = usable?.Use();
            
            SetCurrentData(itemData);
        }

        private void SetCurrentData(ItemData itemData)
        {
            currentData = itemData;

            if (currentData is null)
            {
                inventorySpriteRenderer.sprite = null;
                return;
            }
            
            inventorySpriteRenderer.sprite = currentData.Sprite;
        }

        private void Throw()
        {
            if (currentData is null)
            {
                return;
            }

            var manager = Singleton.GetOrCreateMonoBehaviour<ItemManager>();
            var item = manager.Generate(currentData, carryAnchor);
            item.Throw(direction, throwForce.Value, throwAngle.Value);
            
            SetCurrentData(null);
        }

#endregion

#region Subscriptions

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (currentData is not null)
            {
                Throw();
                return;
            }
            
            Interact();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            if (input.sqrMagnitude < Mathf.Epsilon)
            {
                return;
            }

            direction = new Vector3(input.x, 0, input.y);
        }

#endregion
    }
}