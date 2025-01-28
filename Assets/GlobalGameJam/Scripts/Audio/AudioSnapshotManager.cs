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
        /// Event binding for the LevelEvents.Start event.
        /// </summary>
        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;

        /// <summary>
        /// Event binding for the LevelEvents.End event.
        /// </summary>
        private EventBinding<LevelEvents.End> onLevelEndEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the LevelEvents.Start event.
        /// </summary>
        private void Awake()
        {
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
            onLevelEndEventBinding = new EventBinding<LevelEvents.End>(OnLevelEndEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the LevelEvents.Start event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Register(onLevelEndEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the LevelEvents.Start event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Deregister(onLevelEndEventBinding);
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
        /// Transitions the audio mixer to the "Game" snapshot.
        /// </summary>
        /// <param name="event">The LevelEvents.Start event.</param>
        private void OnLevelStartEventHandler(LevelEvents.Start @event)
        {
            TransitionToSnapshot("Game");
        }

        /// <summary>
        /// Transitions the audio mixer to the "Menu" snapshot.
        /// </summary>
        /// <param name="event">The LevelEvents.End event.</param>
        private void OnLevelEndEventHandler(LevelEvents.End @event)
        {
            TransitionToSnapshot("Menu");
        }

#endregion
    }
}