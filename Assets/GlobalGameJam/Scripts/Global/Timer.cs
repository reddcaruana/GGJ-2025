using UnityEngine;

namespace GlobalGameJam
{
    /// <summary>
    /// Manages a timer that can be activated, deactivated, and paused.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        /// <summary>
        /// Event triggered on each update with the current and total duration.
        /// </summary>
        public event System.Action<float, float> OnUpdate;

        /// <summary>
        /// Event triggered when the timer completes.
        /// </summary>
        public event System.Action OnComplete;

        /// <summary>
        /// Gets the duration of the timer.
        /// </summary>
        [field: SerializeField] public float Duration { get; private set; }

        /// <summary>
        /// Gets the current time of the timer.
        /// </summary>
        public float Current { get; private set; }

        private bool isRunning;

#region Lifecycle Events

        /// <summary>
        /// Updates the timer each frame if it is running.
        /// </summary>
        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            Current += Time.deltaTime;
            OnUpdate?.Invoke(Current, Duration);

            if (Current >= Duration)
            {
                Deactivate();
                OnComplete?.Invoke();
            }
        }

#endregion

#region Methods

        /// <summary>
        /// Activates the timer and resets the current time.
        /// </summary>
        public void Activate()
        {
            Current = 0f;
            isRunning = true;
        }

        /// <summary>
        /// Deactivates the timer and resets the current time.
        /// </summary>
        public void Deactivate()
        {
            isRunning = false;
            Current = 0f;
        }

        /// <summary>
        /// Extends the timer by a given value.
        /// </summary>
        /// <param name="value">The timer value.</param>
        public void Extend(float value)
        {
            Current -= value;
        }

        /// <summary>
        /// Pauses the timer without resetting the current time.
        /// </summary>
        public void Pause()
        {
            isRunning = false;
        }

#endregion
    }
}