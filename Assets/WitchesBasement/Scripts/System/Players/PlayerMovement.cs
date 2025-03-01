using Obvious.Soap;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Rigidbody))]
    internal class PlayerMovement : MonoBehaviour, IBindableComponent
    {
        private enum DashState { Ready, Dashing, Cooldown }
        
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private FloatVariable dashSpeed;
        [SerializeField] private FloatVariable dashDuration;
        [SerializeField] private FloatVariable dashCooldown;
        
        private Rigidbody attachedRigidbody;

        private InputAction dashAction;
        private InputAction moveAction;

        private Vector3 inputDirection;
        private Vector3 dashDirection;

        private DashState dashState = DashState.Ready;

        private float dashDurationCounter;
        private float dashCooldownCounter;

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (dashState is DashState.Dashing)
            {
                attachedRigidbody.linearVelocity = dashDirection * dashSpeed.Value;
                return;
            }
            
            attachedRigidbody.linearVelocity = inputDirection * moveSpeed.Value;
        }

        private void Update()
        {
            switch (dashState)
            {
                case DashState.Dashing:
                    DashDurationTick(Time.deltaTime);
                    DashCooldownTick(Time.deltaTime);
                    break;
                
                case DashState.Cooldown:
                    DashCooldownTick(Time.deltaTime);
                    break;
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
                dashState = DashState.Cooldown;
            }
        }

        private void DashCooldownTick(float deltaTime)
        {
            dashCooldownCounter -= deltaTime;
            if (dashCooldownCounter <= 0)
            {
                dashState = DashState.Ready;
                UpdateDashDirection();
            }
        }

        private void UpdateDashDirection()
        {
            if (inputDirection.sqrMagnitude < Mathf.Epsilon)
            {
                return;
            }

            dashDirection = inputDirection;
        }

#endregion
        
#region Subscriptions

        private void OnDash(InputAction.CallbackContext context)
        {
            if (dashState is not DashState.Ready)
            {
                return;
            }

            if (dashDirection.sqrMagnitude < Mathf.Epsilon)
            {
                dashDirection = transform.forward;
            }

            dashState = DashState.Dashing;
            dashDurationCounter = dashDuration.Value;
            dashCooldownCounter = dashCooldown.Value;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            
            var value = context.ReadValue<Vector2>();
            inputDirection = new Vector3(value.x, 0, value.y);

            if (dashState is not DashState.Dashing)
            {
                UpdateDashDirection();
            }
        }

#endregion
    }
}