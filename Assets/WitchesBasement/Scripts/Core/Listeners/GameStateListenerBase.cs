using UnityEngine;

namespace WitchesBasement
{
    public abstract class GameStateListenerBase : MonoBehaviour
    {
        [SerializeField] private GameStateVariable gameState;

#region Lifecycle Events

        protected virtual void OnEnable()
        {
            gameState.OnValueChanged += OnGameStateChanged;
        }

        protected virtual void OnDisable()
        {
            gameState.OnValueChanged -= OnGameStateChanged;
        }

#endregion

#region Subscriptions

        protected abstract void OnGameStateChanged(GameState gameState);

#endregion
    }
}