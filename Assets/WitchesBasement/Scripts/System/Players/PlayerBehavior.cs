using UnityEngine;
using UnityEngine.InputSystem;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerInventory))]
    internal class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerList;

        public PlayerContext Context { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            Context = new PlayerContext
            {
                Movement = GetComponent<PlayerMovement>(),
                Inventory = GetComponent<PlayerInventory>()
            };
        }

        private void OnEnable()
        {
            playerList.OnItemAdded += Bind;
        }

#endregion

#region Subscriptions

        private void Bind(int id, PlayerInput playerInput)
        {
            playerInput.SwitchCurrentActionMap("Player");
            
            Context.Movement.Bind(playerInput);
            Context.Inventory.Bind(playerInput);
        }

#endregion
    }
}