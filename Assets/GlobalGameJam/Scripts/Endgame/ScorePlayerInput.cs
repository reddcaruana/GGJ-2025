using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalGameJam.Endgame
{
    public class ScorePlayerInput : MonoBehaviour, IBindable
    {
        private int playerID;
        private PlayerInput playerInput;
        
        private InputAction aAction;
        private InputAction bAction;
        private InputAction xAction;
        private InputAction yAction;

        [SerializeField] private TMP_Text characterText;
        
        /// <inheritdoc />
        public void Bind(int playerNumber)
        {
            playerID = playerNumber;
            
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);
            
            playerInput.SwitchCurrentActionMap("Scoring");

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
        }

        private void YHandler(InputAction.CallbackContext obj)
        {
            SetName('Y');
        }

        private void XHandler(InputAction.CallbackContext obj)
        {
            SetName('X');
        }

        private void BHandler(InputAction.CallbackContext obj)
        {
            SetName('B');
        }

        private void AHandler(InputAction.CallbackContext obj)
        {
            SetName('A');
        }

        private void SetName(char character)
        {
            var manager = Singleton.GetOrCreateMonoBehaviour<ScoreScreenManager>();
            manager.SetName(playerID, character);

            Release();
            characterText.text = character.ToString();
        }
    }
}