using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Listens for instructions and resume events, and manages the binding and releasing of player inputs.
    /// </summary>
    public class InstructionsListener : MonoBehaviour
    {
        /// <summary>
        /// Array of InstructionsPlayer instances representing the players.
        /// </summary>
        [SerializeField] private InstructionsPlayer[] players;

        /// <summary>
        /// Event binding for the Instructions event.
        /// </summary>
        private EventBinding<LevelEvents.Instructions> onInstructionsEventBinding;

        /// <summary>
        /// Event binding for the Resume event.
        /// </summary>
        private EventBinding<DirectorEvents.Resume> onResumeDirectorEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event bindings for the Instructions and Resume events.
        /// </summary>
        private void Awake()
        {
            onResumeDirectorEventBinding = new EventBinding<DirectorEvents.Resume>(OnResumeDirectorEventHandler);
            onInstructionsEventBinding = new EventBinding<LevelEvents.Instructions>(OnInstructionsEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the event bindings for the Instructions and Resume events.
        /// </summary>
        private void OnEnable()
        {
            EventBus<DirectorEvents.Resume>.Register(onResumeDirectorEventBinding);
            EventBus<LevelEvents.Instructions>.Register(onInstructionsEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the event bindings for the Instructions and Resume events.
        /// </summary>
        private void OnDisable()
        {
            EventBus<DirectorEvents.Resume>.Deregister(onResumeDirectorEventBinding);
            EventBus<LevelEvents.Instructions>.Deregister(onInstructionsEventBinding);
        }

        /// <summary>
        /// Resets the players array to the InstructionsPlayer components found in the children of this GameObject.
        /// </summary>
        private void Reset()
        {
            players = GetComponentsInChildren<InstructionsPlayer>();
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the Instructions event.
        /// Binds the player inputs for each player.
        /// </summary>
        /// <param name="event">The Instructions event.</param>
        private void OnInstructionsEventHandler(LevelEvents.Instructions @event)
        {
            for (var i = 0; i < players.Length; i++)
            {
                var instructionsPlayer = players[i];
                instructionsPlayer.Bind(i);
            }
        }

        /// <summary>
        /// Handles the Resume event.
        /// Releases the player inputs for each player.
        /// </summary>
        /// <param name="event">The Resume event.</param>
        private void OnResumeDirectorEventHandler(DirectorEvents.Resume @event)
        {
            foreach (var instructionsPlayer in players)
            {
                instructionsPlayer.Release();
            }
        }

#endregion
    }
}