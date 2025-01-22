using GlobalGameJam.Data;
using GlobalGameJam.Players;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GlobalGameJam.Lobby
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlayerAccount : MonoBehaviour
    {
        [SerializeField] private Image profileImage;
        [SerializeField] private TMP_Text profileName;

        private CanvasGroup canvasGroup;
        
        private PlayerInput playerInput;
        private InputAction leaveAction;

#region Lifecycle Events

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Reset()
        {
            profileImage = GetComponentInChildren<Image>();
            profileName = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            canvasGroup.alpha = 1.0f;
        }

        private void OnDisable()
        {
            canvasGroup.alpha = 0.5f;
        }

#endregion

#region Methods

        public void Setup(PlayerProfileData profileData)
        {
            profileImage.sprite = profileData.Sprite;
            profileName.text = profileData.Username;
        }

#endregion

#region Player Input Control

        public void Bind(int playerNumber)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);
            
            playerInput.SwitchCurrentActionMap("Lobby");

            leaveAction = playerInput.currentActionMap.FindAction("Leave");
            leaveAction.started += LeaveHandler;
        }

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

        private void LeaveHandler(InputAction.CallbackContext context)
        {
            var input = playerInput;
            Release();
            
            Destroy(input.gameObject);
        }

#endregion
    }
}