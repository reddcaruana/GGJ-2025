using GlobalGameJam.Events;
using GlobalGameJam.Gameplay;
using UnityEngine;
using UnityEngine.Audio;

namespace GlobalGameJam.Audio
{
    /// <summary>
    /// Manages audio snapshot transitions in response to level start events.
    /// </summary>
    public class AudioSnapshotManager : MonoBehaviour
    {
        /// <summary>
        /// The audio mixer containing the snapshots.
        /// </summary>
        [SerializeField] private AudioMixer audioMixer;

        /// <summary>
        /// The duration of the transition between audio snapshots.
        /// </summary>
        [SerializeField] private float transitionDuration = 2.0f;

        /// <summary>
        /// Event binding for the LevelEvents.SetMode event.
        /// </summary>
        private EventBinding<LevelEvents.SetMode> onSetLevelModeEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the LevelEvents.Start event.
        /// </summary>
        private void Awake()
        {
            onSetLevelModeEventBinding = new EventBinding<LevelEvents.SetMode>(OnSetLevelModeEventHandler);
        }

        /// <summary>
        /// Registers the LevelEvents.Start event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.SetMode>.Register(onSetLevelModeEventBinding);
        }

        /// <summary>
        /// Deregisters the LevelEvents.Start event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.SetMode>.Deregister(onSetLevelModeEventBinding);
        }

#endregion

#region Methods
        
        /// <summary>
        /// Transitions the audio mixer to the specified snapshot.
        /// </summary>
        /// <param name="snapshotName">The name of the snapshot to transition to.</param>
        private void TransitionToSnapshot(string snapshotName)
        {
            var targetSnapshot = new[] { audioMixer.FindSnapshot(snapshotName) };
            var weight = new[] { 1.0f };

            audioMixer.TransitionToSnapshots(targetSnapshot, weight, transitionDuration);
        }
#endregion

#region Event Handlers

        /// <summary>
        /// Transitions the audio mixer to the correct snapshot.
        /// </summary>
        /// <param name="event">The LevelEvents.SetMode event.</param>
        private void OnSetLevelModeEventHandler(LevelEvents.SetMode @event)
        {
            switch (@event.Mode)
            {
                case LevelMode.Start:
                    TransitionToSnapshot("Game");
                    break;
                
                case LevelMode.End:
                    TransitionToSnapshot("Menu");
                    break;
            }
        }

#endregion
    }
}