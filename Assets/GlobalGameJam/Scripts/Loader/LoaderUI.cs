using System.Collections;
using GlobalGameJam.Events;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGameJam.Loader
{
    /// <summary>
    /// Manages the UI for the loading screen, including the progress bar and rebooting text.
    /// </summary>
    public class LoaderUI : MonoBehaviour
    {
        /// <summary>
        /// The image component representing the progress bar.
        /// </summary>
        [SerializeField] private Image progressBar;

        /// <summary>
        /// The text component displaying the loading percentage.
        /// </summary>
        [SerializeField] private TMP_Text percentageText;

        /// <summary>
        /// The game object displaying the rebooting text.
        /// </summary>
        [SerializeField] private TMP_Text rebootingText;

        /// <summary>
        /// The current loading percentage.
        /// </summary>
        private float percentage;

#region Lifecycle Events

        /// <summary>
        /// Starts the coroutines for flashing the rebooting text and updating the loading progress.
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(FlashRebootingTextRoutine());
            StartCoroutine(LoadingRoutine());
        }

#endregion

#region Methods

        /// <summary>
        /// Sets the loading percentage and updates the UI elements accordingly.
        /// </summary>
        /// <param name="value">The new loading percentage value.</param>
        private void SetPercentage(float value)
        {
            percentage = value;

            progressBar.fillAmount = value;
            percentageText.text = $"{percentage * 100:0}%";
        }

#endregion

#region Coroutines

        /// <summary>
        /// Coroutine that flashes the rebooting text on and off while loading.
        /// </summary>
        private IEnumerator FlashRebootingTextRoutine()
        {
            while (percentage < 1.0f)
            {
                yield return new WaitForSeconds(0.5f);
                rebootingText.gameObject.SetActive(!rebootingText.gameObject.activeSelf);
            }
            
            rebootingText.gameObject.SetActive(true);
            rebootingText.text = "COMPLETE";
        }

        /// <summary>
        /// Coroutine that simulates the loading process by updating the loading percentage over time.
        /// </summary>
        private IEnumerator LoadingRoutine()
        {
            SetPercentage(0.0f);
            yield return new WaitForSeconds(1.0f);
        
            yield return StartCoroutine(UpdatePercentageOverTime(1.0f, Random.Range(0.08f, 0.12f), 0.0f));
            yield return new WaitForSeconds(1.0f);
        
            SetPercentage(Random.Range(0.25f, 0.35f));
            yield return new WaitForSeconds(0.6f);
        
            SetPercentage(Random.Range(0.49f, 0.51f));
            yield return new WaitForSeconds(0.65f);
        
            yield return StartCoroutine(UpdatePercentageOverTime(0.15f, Random.Range(0.72f, 0.77f), percentage));
            yield return new WaitForSeconds(1.0f);
        
            SetPercentage(Random.Range(0.83f, 0.85f));
            yield return new WaitForSeconds(0.5f);
        
            yield return StartCoroutine(UpdatePercentageOverTime(0.5f, 0.99f, percentage));
            yield return new WaitForSeconds(1.5f);
        
            SetPercentage(1.0f);
            yield return new WaitForSeconds(1.0f);
            
            EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
            EventBus<PlayerEvents.EnableJoining>.Raise(PlayerEvents.EnableJoining.Default);
            enabled = false;
        }

        /// <summary>
        /// Coroutine that updates the loading percentage over a specified duration.
        /// </summary>
        /// <param name="duration">The duration over which to update the percentage.</param>
        /// <param name="target">The target percentage value.</param>
        /// <param name="start">The starting percentage value (default is 0).</param>
        private IEnumerator UpdatePercentageOverTime(float duration, float target, float start = 0f)
        {
            var current = 0.0f;
            var difference = target - start;
            while (current < duration)
            {
                current = Mathf.Min(current + Time.deltaTime, duration);
                SetPercentage(start + difference * (current / duration));
                yield return null;
            }
        }

#endregion
    }
}