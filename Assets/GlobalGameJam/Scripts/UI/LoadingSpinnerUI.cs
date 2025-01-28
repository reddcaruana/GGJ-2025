using UnityEngine;

namespace GlobalGameJam.UI
{
    public class LoadingSpinnerUI : MonoBehaviour
    {
        /// <summary>
        /// The interval between each rotation step in seconds.
        /// </summary>
        [SerializeField] private float interval = 0.25f;

        /// <summary>
        /// The remaining time until the next rotation step.
        /// </summary>
        private float time;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the time with a random value.
        /// </summary>
        private void Start()
        {
            time = Random.value;
        }

        /// <summary>
        /// Called every frame.
        /// Updates the rotation of the spinner based on the interval.
        /// </summary>
        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                return;
            }

            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = (eulerAngles.z - 45) % 360;
            transform.eulerAngles = eulerAngles;

            time = interval;
        }

#endregion
    }
}