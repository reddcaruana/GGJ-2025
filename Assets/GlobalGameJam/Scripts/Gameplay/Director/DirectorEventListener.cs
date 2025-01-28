using UnityEngine;
using UnityEngine.Playables;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(PlayableDirector))]
    public class DirectorEventListener : MonoBehaviour
    {
        [SerializeField] private PlayableAsset introAsset;
        [SerializeField] private PlayableAsset outroAsset;

        /// <summary>
        /// The PlayableDirector component attached to this GameObject.
        /// </summary>
        private PlayableDirector playableDirector;

        /// <summary>
        /// Event binding for the DirectorEvents.Play event.
        /// </summary>
        private EventBinding<DirectorEvents.Resume> onDirectorResumeEventBinding;

        /// <summary>
        /// Event binding for the LevelEvents.End event.
        /// </summary>
        private EventBinding<LevelEvents.End> onLevelEndEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the PlayableDirector component and the event binding for the Play event.
        /// </summary>
        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            
            onDirectorResumeEventBinding = new EventBinding<DirectorEvents.Resume>(OnDirectorResumeEventHandler);
            onLevelEndEventBinding = new EventBinding<LevelEvents.End>(OnLevelEndEventHandler);
        }

        /// <summary>
        /// Registers the Play event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<DirectorEvents.Resume>.Register(onDirectorResumeEventBinding);
            EventBus<LevelEvents.End>.Register(onLevelEndEventBinding);
        }

        /// <summary>
        /// Deregisters the Play event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<DirectorEvents.Resume>.Deregister(onDirectorResumeEventBinding);
            EventBus<LevelEvents.End>.Deregister(onLevelEndEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the Play event.
        /// Plays the PlayableDirector.
        /// </summary>
        /// <param name="event">The Play event.</param>
        private void OnDirectorResumeEventHandler(DirectorEvents.Resume @event)
        {
            playableDirector.Play();
        }

        /// <summary>
        /// Handles the Level End event.
        /// Plays the outro PlayableAsset using the PlayableDirector.
        /// </summary>
        /// <param name="event">The Level End event.</param>
        private void OnLevelEndEventHandler(LevelEvents.End @event)
        {
            playableDirector.Play(outroAsset);
        }

#endregion
    }
}