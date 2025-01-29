using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Reloads the current scene after a score submission event, with a specified delay.
    /// </summary>
    public class ReloadSceneAfterSubmission : MonoBehaviour
    {
        /// <summary>
        /// The delay in seconds before reloading the scene.
        /// </summary>
        [SerializeField] private float delay = 3.0f;

        /// <summary>
        /// Event binding for the ScoreEvents.Submit event.
        /// </summary>
        private EventBinding<ScoreEvents.Submit> onScoreSubmitEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the ScoreEvents.Submit event.
        /// </summary>
        private void Awake()
        {
            onScoreSubmitEventBinding = new EventBinding<ScoreEvents.Submit>(OnScoreSubmitEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the ScoreEvents.Submit event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<ScoreEvents.Submit>.Register(onScoreSubmitEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the ScoreEvents.Submit event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<ScoreEvents.Submit>.Deregister(onScoreSubmitEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the ScoreEvents.Submit event.
        /// Starts the coroutine to reload the scene after a delay.
        /// </summary>
        /// <param name="event">The ScoreEvents.Submit event.</param>
        private void OnScoreSubmitEventHandler(ScoreEvents.Submit @event)
        {
            StartCoroutine(ReloadSceneRoutine());
        }

#endregion

#region Coroutines

        /// <summary>
        /// Coroutine to reload the current scene after a specified delay.
        /// </summary>
        private IEnumerator ReloadSceneRoutine()
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

#endregion
    }
}