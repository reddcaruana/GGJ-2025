using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Players;

namespace WitchesBasement.Intro
{
    internal class PlayerConnection : MonoBehaviour, IBindableComponent
    {
        private PlayerInput input;
        
        private InputAction leaveAction;
        
#region Implementation of IBindableComponent

        public void Bind(PlayerInput playerInput)
        {
            input = playerInput;
            
            playerInput.SwitchCurrentActionMap("Lobby");
            var currentActionMap = playerInput.currentActionMap;

            leaveAction = currentActionMap.FindAction("Leave");
            leaveAction.started += OnLeave;
        }

        public void Release()
        {
            if (leaveAction != null)
            {
                leaveAction.started -= OnLeave;
                leaveAction = null;
            }
        }

#endregion

#region Subscriptions

        private void OnLeave(InputAction.CallbackContext context)
        {
            Release();
            Destroy(input.gameObject);
        }

#endregion
    }
}