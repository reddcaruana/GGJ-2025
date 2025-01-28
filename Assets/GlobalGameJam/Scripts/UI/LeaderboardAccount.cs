using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GlobalGameJam.UI
{
    public class LeaderboardAccount : MonoBehaviour, IBindable
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text username;
        [SerializeField] private TMP_Text characterText;

        [SerializeField] private CanvasGroup canvasGroup;

        private PlayerInput playerInput;
        private int playerID = -1;

        private InputAction aAction;
        private InputAction bAction;
        private InputAction xAction;
        private InputAction yAction;

#region Implementation of IBindable


        /// <inheritdoc />
        public void Bind(int playerNumber)
        {
            playerID = playerNumber;
            
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);

            if (playerInput is null)
            {
                return;
            }

            playerInput.SwitchCurrentActionMap("Leaderboard");

            aAction = playerInput.currentActionMap.FindAction("A");
            aAction.started += AHandler;

            bAction = playerInput.currentActionMap.FindAction("B");
            bAction.started += BHandler;

            xAction = playerInput.currentActionMap.FindAction("X");
            xAction.started += XHandler;

            yAction = playerInput.currentActionMap.FindAction("Y");
            yAction.started += YHandler;
        }

        /// <inheritdoc />
        public void Release()
        {
            if (aAction is not null)
            {
                aAction.started -= AHandler;
            }
            aAction = null;

            if (bAction is not null)
            {
                bAction.started -= BHandler;
            }
            bAction = null;

            if (xAction is not null)
            {
                xAction.started -= XHandler;
            }
            xAction = null;

            if (yAction is not null)
            {
                yAction.started -= YHandler;
            }
            yAction = null;

            playerInput = null;
            playerID = -1;
        }

#endregion

#region Event Handlers


        private void YHandler(InputAction.CallbackContext obj)
        {
            SetCharacter('Y');
        }

        private void XHandler(InputAction.CallbackContext obj)
        {
            SetCharacter('X');
        }

        private void BHandler(InputAction.CallbackContext obj)
        {
            SetCharacter('B');
        }

        private void AHandler(InputAction.CallbackContext obj)
        {
            SetCharacter('A');
        }

#endregion

#region Methods

        public void Setup(ProfileData profileData)
        {
            image.sprite = profileData.Sprite;
            username.text = profileData.Username;
        }

        private void SetCharacter(char character)
        {
            canvasGroup.alpha = 0.3f;
            characterText.text = character.ToString();
            characterText.gameObject.SetActive(true);
            
            EventBus<ScoreEvents.SetInitial>.Raise(new ScoreEvents.SetInitial
            {
                PlayerID = playerID,
                Initial = character
            });

            Release();
        }

#endregion
    }
}