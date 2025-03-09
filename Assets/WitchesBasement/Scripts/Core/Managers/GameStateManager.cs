using UnityEngine;

namespace WitchesBasement
{
    internal class GameStateManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventGameState gameStateEvent;
        [SerializeField] private GameStateVariable gameState;

#region Lifecycle Events

        private void OnEnable()
        {
            gameStateEvent.OnRaised += OnGameStateEventRaised;
        }

        private void OnDisable()
        {
            gameStateEvent.OnRaised -= OnGameStateEventRaised;
        }

#endregion

#region Subscriptions

        private void OnGameStateEventRaised(GameState newGameState)
        {
            gameState.Value = newGameState;
        }

#endregion
    }
}