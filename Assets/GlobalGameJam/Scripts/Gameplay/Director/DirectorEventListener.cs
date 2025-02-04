using GlobalGameJam.Events;
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
        private EventBinding<LevelEvents.SetMode> onSetLevelModeEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the PlayableDirector component and the event binding for the Play event.
        /// </summary>
        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            
            onDirectorResumeEventBinding = new EventBinding<DirectorEvents.Resume>(OnDirectorResumeEventHandler);
            onSetLevelModeEventBinding = new EventBinding<LevelEvents.SetMode>(OnSetLevelModeEventHandler);
        }

        /// <summary>
        /// Registers the Play event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<DirectorEvents.Resume>.Register(onDirectorResumeEventBinding);
            EventBus<LevelEvents.SetMode>.Register(onSetLevelModeEventBinding);
        }

        /// <summary>
        /// Deregisters the Play event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<DirectorEvents.Resume>.Deregister(onDirectorResumeEventBinding);
            EventBus<LevelEvents.SetMode>.Deregister(onSetLevelModeEventBinding);
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
        private void OnSetLevelModeEventHandler(LevelEvents.SetMode @event)
        {
            if (@event.Mode is not LevelMode.End)
            {
                return;
            }
            
            playableDirector.Play(outroAsset);
        }

#endregion
    }
}