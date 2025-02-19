using System.Collections;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Rigidbody))]
    internal class PlayerMovement : MonoBehaviour, IBindableComponent
    {
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable dashSpeed;
        [SerializeField] private FloatVariable dashDuration;
        [SerializeField] private FloatVariable dashCooldown;

        private Rigidbody attachedRigidbody;

        private InputAction dashAction;
        private InputAction moveAction;

        private Vector3 inputDirection;
        private Vector3 dashDirection;

        private bool canDash = true;
        private bool isDashing;

        private float dashDurationCounter;
        private float dashCooldownCounter;

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                attachedRigidbody.linearVelocity = dashDirection * dashSpeed.Value;
                return;
            }
            
            attachedRigidbody.linearVelocity = inputDirection * moveSpeed.Value;
        }

        private void Update()
        {
            if (isDashing)
            {
                DashDurationTick(Time.deltaTime);
                return;
            }

            if (canDash == false)
            {
                DashCooldownTick(Time.deltaTime);
            }
        }

#endregion

#region Implementation of IBindableComponent

        public void Bind(PlayerInput playerInput)
        {
            var currentActionMap = playerInput.currentActionMap;

            dashAction = currentActionMap.FindAction("Dash");
            dashAction.started += OnDash;

            moveAction = currentActionMap.FindAction("Move");
            moveAction.started += OnMove;
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }

        /// <inheritdoc />
        public void Release()
        {
            if (dashAction is not null)
            {
                dashAction.started -= OnDash;
                dashAction = null;
            }
            
            if (moveAction is not null)
            {
                moveAction.started -= OnMove;
                moveAction.performed -= OnMove;
                moveAction.canceled -= OnMove;
                moveAction = null;
            }
        }

#endregion

#region Methods

        private void DashDurationTick(float deltaTime)
        {
            dashDurationCounter -= deltaTime;
            if (dashDurationCounter <= 0)
            {
                isDashing = false;
            }
        }

        private void DashCooldownTick(float deltaTime)
        {
            dashCooldownCounter -= deltaTime;
            if (dashCooldownCounter <= 0)
            {
                canDash = true;
            }
        }

#endregion
        
#region Subscriptions

        private void OnDash(InputAction.CallbackContext context)
        {
            if (canDash == false || dashDirection.sqrMagnitude < Mathf.Epsilon)
            {
                return;
            }

            isDashing = true;
            canDash = false;

            dashDurationCounter = dashDuration.Value;
            dashCooldownCounter = dashCooldown.Value;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            inputDirection = new Vector3(value.x, 0, value.y);

            if (inputDirection.sqrMagnitude > Mathf.Epsilon)
            {
                dashDirection = inputDirection;
            }
        }

#endregion
    }
}