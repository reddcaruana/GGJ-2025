using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GlobalGameJam.Lobby
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlayerAccount : MonoBehaviour, IBindable
    {
        [SerializeField] private Image profileImage;
        [SerializeField] private TMP_Text profileName;

        private CanvasGroup canvasGroup;

        private PlayerInput playerInput;
        private InputAction leaveAction;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Resets the component to its default state.
        /// </summary>
        private void Reset()
        {
            profileImage = GetComponentInChildren<Image>();
            profileName = GetComponentInChildren<TMP_Text>();
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            canvasGroup.alpha = 1.0f;
        }

        /// <summary>
        /// Called when the object becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            canvasGroup.alpha = 0.5f;
        }

#endregion

#region Methods

        /// <summary>
        /// Sets up the player account with the provided profile data.
        /// </summary>
        /// <param name="profileData">The profile data to set up.</param>
        public void Setup(PlayerProfileData profileData)
        {
            profileImage.sprite = profileData.Sprite;
            profileName.text = profileData.Username;
        }

#endregion

#region IBindable Implementation

        /// <summary>
        /// Binds the player input to the specified player number.
        /// </summary>
        /// <param name="playerNumber">The player number to bind.</param>
        public void Bind(int playerNumber)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);

            playerInput.SwitchCurrentActionMap("Lobby");

            leaveAction = playerInput.currentActionMap.FindAction("Leave");
            leaveAction.started += LeaveHandler;
        }

        /// <summary>
        /// Releases the player input and unbinds the leave action.
        /// </summary>
        public void Release()
        {
            if (leaveAction is not null)
            {
                leaveAction.started -= LeaveHandler;
            }

            leaveAction = null;

            playerInput = null;
        }

#endregion

#region Action Handlers

        /// <summary>
        /// Handles the leave action when it is started.
        /// </summary>
        /// <param name="context">The callback context of the input action.</param>
        private void LeaveHandler(InputAction.CallbackContext context)
        {
            var input = playerInput;
            Release();

            Destroy(input.gameObject);
        }

#endregion
    }
}