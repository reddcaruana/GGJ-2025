using System.Collections;
using GlobalGameJam.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Reloads the current scene with a specified delay.
    /// </summary>
    public class ReloadSceneHandler : MonoBehaviour
    {
        /// <summary>
        /// Event binding for the LevelEvents.Reload event.
        /// </summary>
        private EventBinding<LevelEvents.Reload> onReloadLevelEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the ScoreEvents.Submit event.
        /// </summary>
        private void Awake()
        {
            onReloadLevelEventBinding = new EventBinding<LevelEvents.Reload>(OnReloadLevelEventHandler);
        }

        /// <summary>
        /// Registers the LevelEvents.Reload event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.Reload>.Register(onReloadLevelEventBinding);
        }

        /// <summary>
        /// Deregisters the LevelEvents.Reload event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.Reload>.Deregister(onReloadLevelEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event for reloading the level.
        /// </summary>
        /// <param name="event">The reload level event.</param>
        private void OnReloadLevelEventHandler(LevelEvents.Reload @event)
        {
            StartCoroutine(ReloadSceneRoutine(@event.Delay));
        }

#endregion

#region Coroutines

        /// <summary>
        /// Coroutine that reloads the scene after a specified delay.
        /// </summary>
        /// <param name="delay">The delay before reloading the scene.</param>
        private IEnumerator ReloadSceneRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

#endregion
    }
}