using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(PlayerMovement))]
    internal class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerIDs;

        public PlayerContext Context { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            Context = new PlayerContext
            {
                Movement = GetComponent<PlayerMovement>()
            };
        }

        private void OnEnable()
        {
            playerIDs.OnItemAdded += Bind;
        }

#endregion

#region Subscriptions

        private void Bind(int id, PlayerInput playerInput)
        {
            playerInput.SwitchCurrentActionMap("Player");
            
            Context.Movement.Bind(playerInput);
        }

#endregion
    }
}