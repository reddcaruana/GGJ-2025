using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Timer : MonoBehaviour
    {
        public event System.Action OnUpdate;
        public event System.Action OnComplete;
        
        [field: SerializeField] public float Duration { get; private set; }
        public float Current { get; private set; }

        private bool isRunning;
        
        public float NormalizedTime => Current / Duration;

#region Lifecycle Events

        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            Current += Time.deltaTime;
            OnUpdate?.Invoke();
            
            if (Current >= Duration)
            {
                Deactivate();
                OnComplete?.Invoke();
            }
        }

#endregion

#region Methods

        public void Activate()
        {
            Current = 0f;
            isRunning = true;
        }

        public void Deactivate()
        {
            isRunning = false;
            Current = 0f;
        }

        public void Pause()
        {
            isRunning = false;
        }

#endregion
    }
}