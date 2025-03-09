using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class Timer : MonoBehaviour
    {
        [SerializeField] private FloatVariable duration;
        [SerializeField] private FloatVariable currentValue;

        [SerializeField] private ScriptableEventGameState gameStateEvent;
        
        private bool isRunning;

#region Lifecycle Events

        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            var newValue = currentValue.Value - Time.deltaTime;
            if (newValue <= 0)
            {
                gameStateEvent?.Raise(GameState.End);
                isRunning = false;
                return;
            }

            currentValue.Value = newValue;
        }

#endregion

#region Methods

        [ContextMenu("Activate")]
        public void Initialize()
        {
            currentValue.Value = duration.Value;
        }

        public void Extend(float value)
        {
            currentValue.Value += value;
        }

        public void Pause()
        {
            isRunning = false;
        }

        public void Resume()
        {
            isRunning = true;
        }

#endregion
    }
}