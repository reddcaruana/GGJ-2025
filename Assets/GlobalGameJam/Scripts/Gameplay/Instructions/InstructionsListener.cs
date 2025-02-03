using UnityEngine;
using UnityEngine.UI;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Listens for instructions and resume events, and manages the binding and releasing of player inputs.
    /// </summary>
    public class InstructionsListener : MonoBehaviour
    {
        /// <summary>
        /// Array of screen game objects.
        /// </summary>
        [SerializeField] private GameObject[] screens;
        
        /// <summary>
        /// Array of InstructionsPlayer instances representing the players.
        /// </summary>
        [SerializeField] private InstructionsPlayer[] players;

        [Header("Navigation Settings")]
        [SerializeField] private Image backImage;
        [SerializeField] private Image nextImage;

        [SerializeField] private Color disabledColor = Color.white;
        [SerializeField] private Color enabledColor = Color.white;

        /// <summary>
        /// Event binding for the Instructions event.
        /// </summary>
        private EventBinding<LevelEvents.Instructions> onInstructionsEventBinding;

        /// <summary>
        /// Event binding for the back screen switch.
        /// </summary>
        private EventBinding<InstructionsEvent.Back> onBackInstructionEventBinding;

        /// <summary>
        /// Event binding for the next screen switch.
        /// </summary>
        private EventBinding<InstructionsEvent.Next> onNextInstructionEventBinding;

        /// <summary>
        /// Event binding for the Resume event.
        /// </summary>
        private EventBinding<DirectorEvents.Resume> onResumeDirectorEventBinding;

        /// <summary>
        /// The current screen.
        /// </summary>
        private int currentScreen;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event bindings for the Instructions and Resume events.
        /// </summary>
        private void Awake()
        {
            onResumeDirectorEventBinding = new EventBinding<DirectorEvents.Resume>(OnResumeDirectorEventHandler);
            onInstructionsEventBinding = new EventBinding<LevelEvents.Instructions>(OnInstructionsEventHandler);

            onBackInstructionEventBinding = new EventBinding<InstructionsEvent.Back>(OnBackInstructionEventHandler);
            onNextInstructionEventBinding = new EventBinding<InstructionsEvent.Next>(OnNextInstructionEventHandler);
        }

        /// <summary>
        /// Registers the event bindings for the Instructions and Resume events.
        /// </summary>
        private void OnEnable()
        {
            EventBus<DirectorEvents.Resume>.Register(onResumeDirectorEventBinding);
            EventBus<LevelEvents.Instructions>.Register(onInstructionsEventBinding);
            
            EventBus<InstructionsEvent.Next>.Register(onNextInstructionEventBinding);
            EventBus<InstructionsEvent.Back>.Register(onBackInstructionEventBinding);
        }

        /// <summary>
        /// Deregisters the event bindings for the Instructions and Resume events.
        /// </summary>
        private void OnDisable()
        {
            EventBus<DirectorEvents.Resume>.Deregister(onResumeDirectorEventBinding);
            EventBus<LevelEvents.Instructions>.Deregister(onInstructionsEventBinding);
            
            EventBus<InstructionsEvent.Next>.Deregister(onNextInstructionEventBinding);
            EventBus<InstructionsEvent.Back>.Deregister(onBackInstructionEventBinding);
        }

        /// <summary>
        /// Resets the players array to the InstructionsPlayer components found in the children of this GameObject.
        /// </summary>
        private void Reset()
        {
            players = GetComponentsInChildren<InstructionsPlayer>();
        }

#endregion

#region Methods

        /// <summary>
        /// Sets the screen visibility.
        /// </summary>
        /// <param name="index">The screen index.</param>
        private void SetScreen(int index)
        {
            for (var i = 0; i < screens.Length; i++)
            {
                screens[i].SetActive(index == i);
            }

            backImage.color = index <= 0 ? disabledColor : enabledColor;
            nextImage.color = index >= screens.Length - 1 ? disabledColor : enabledColor;
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
            currentScreen = 0;
            SetScreen(currentScreen);
            
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

        /// <summary>
        /// Moves to the next screen.
        /// </summary>
        /// <param name="event">The Next event.</param>
        private void OnNextInstructionEventHandler(InstructionsEvent.Next @event)
        {
            currentScreen++;

            if (currentScreen >= screens.Length)
            {
                EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
                return;
            }
            
            SetScreen(currentScreen);
        }

        /// <summary>
        /// Moves to the previous screen.
        /// </summary>
        /// <param name="event">The Next event.</param>
        private void OnBackInstructionEventHandler(InstructionsEvent.Back @event)
        {
            if (currentScreen <= 0)
            {
                return;
            }

            currentScreen--;
            SetScreen(currentScreen);
        }

#endregion
    }
}