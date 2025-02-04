using GlobalGameJam.Data;
using GlobalGameJam.Events;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Manages the leaderboard account UI and input bindings for a player.
    /// </summary>
    public class LeaderboardAccount : MonoBehaviour, IBindable
    {
        /// <summary>
        /// The image component for the player's profile picture.
        /// </summary>
        [SerializeField] private Image image;

        /// <summary>
        /// The text component for the player's username.
        /// </summary>
        [SerializeField] private TMP_Text username;

        /// <summary>
        /// The text component for displaying the character input.
        /// </summary>
        [SerializeField] private TMP_Text characterText;

        /// <summary>
        /// The canvas group for controlling UI visibility.
        /// </summary>
        [SerializeField] private CanvasGroup canvasGroup;

        private PlayerInput playerInput;
        private int playerID = -1;

        private InputAction aAction;
        private InputAction bAction;
        private InputAction xAction;
        private InputAction yAction;

#region Implementation of IBindable

        /// <summary>
        /// Binds the player input actions for the specified player number.
        /// </summary>
        /// <param name="playerNumber">The player number to bind.</param>
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

        /// <summary>
        /// Releases the player input actions.
        /// </summary>
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

        /// <summary>
        /// Handles the Y button input action.
        /// </summary>
        /// <param name="_">The input action callback context.</param>
        private void YHandler(InputAction.CallbackContext _)
        {
            SetCharacter('Y');
        }

        /// <summary>
        /// Handles the X button input action.
        /// </summary>
        /// <param name="_">The input action callback context.</param>
        private void XHandler(InputAction.CallbackContext _)
        {
            SetCharacter('X');
        }

        /// <summary>
        /// Handles the B button input action.
        /// </summary>
        /// <param name="_">The input action callback context.</param>
        private void BHandler(InputAction.CallbackContext _)
        {
            SetCharacter('B');
        }

        /// <summary>
        /// Handles the A button input action.
        /// </summary>
        /// <param name="_">The input action callback context.</param>
        private void AHandler(InputAction.CallbackContext _)
        {
            SetCharacter('A');
        }

#endregion

#region Methods

        /// <summary>
        /// Sets up the leaderboard account UI with the specified profile data.
        /// </summary>
        /// <param name="profileData">The profile data to set up.</param>
        public void Setup(ProfileData profileData)
        {
            image.sprite = profileData.Sprite;
            username.text = profileData.Username;
        }

        /// <summary>
        /// Sets the character input and raises the SetInitial event.
        /// </summary>
        /// <param name="character">The character to set.</param>
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