using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    public class LobbyPlayerInputBinding : BasePlayerInputBinding
    {
        public event System.Action OnJoined;
        public event System.Action OnLeft;
        
        private InputAction joinAction;
        private InputAction leaveAction;

#region Overrides of BasePlayerInputBinding

        /// <inheritdoc />
        public override void Bind(int playerID)
        {
            base.Bind(playerID);
            
            Input.SwitchCurrentActionMap("Lobby");

            var actionMap = Input.currentActionMap;

            joinAction = actionMap.FindAction("Join");
            joinAction.started += JoinActionHandler;
            
            leaveAction = actionMap.FindAction("Leave");
            leaveAction.started += LeaveActionHandler;
        }

        /// <inheritdoc />
        public override void Release()
        {
            if (joinAction is not null)
            {
                joinAction.started -= JoinActionHandler;
                joinAction = null;
            }

            if (leaveAction is not null)
            {
                leaveAction.started -= LeaveActionHandler;
                leaveAction = null;
            }
            
            base.Release();
        }

#endregion

#region Action Handlers

        private void JoinActionHandler(InputAction.CallbackContext context)
        {
            OnJoined?.Invoke();
        }

        private void LeaveActionHandler(InputAction.CallbackContext context)
        {
            OnLeft?.Invoke();

            var input = Input;
            Release();
            
            if (input is not null)
            {
                Destroy(input.gameObject);
            }
            
        }

#endregion
    }
}