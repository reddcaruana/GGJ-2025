using UnityEngine;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Timer))]
    internal class TimerGameStateListener : GameStateListenerBase
    {
        private Timer attachedTimer;

#region Lifecycle Events

        private void Awake()
        {
            attachedTimer = GetComponent<Timer>();
        }

#endregion
        
#region Overrides of GameStateListenerBase

        /// <inheritdoc />
        protected override void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    attachedTimer.Initialize();
                    break;
                
                case GameState.Active:
                    attachedTimer.Resume();
                    break;
                
                case GameState.Paused:
                    attachedTimer.Pause();
                    break;
            }
        }

#endregion
    }
}