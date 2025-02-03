using System.Collections.Generic;
using GlobalGameJam.Gameplay;
using GlobalGameJam.Players;
using TMPro;
using UnityEngine;

namespace GlobalGameJam.Lobby
{
    /// <summary>
    /// Observes the lobby state and manages player events and timer updates.
    /// </summary>
    public class LobbyObserver : MonoBehaviour
    {
        /// <summary>
        /// Timer for the lobby countdown.
        /// </summary>
        [SerializeField] private Timer timer;

        /// <summary>
        /// Text component to display the timer countdown.
        /// </summary>
        [SerializeField] private TMP_Text timerText;

        /// <summary>
        /// List of player IDs that have been added to the lobby.
        /// </summary>
        private readonly List<int> addedPlayers = new();

        private EventBinding<PlayerEvents.Joined> onPlayerJoinedEventBinding;
        private EventBinding<PlayerEvents.Add> onPlayerAddedEventBinding;
        private EventBinding<PlayerEvents.Remove> onPlayerRemovedEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes event bindings.
        /// </summary>
        private void Awake()
        {
            onPlayerJoinedEventBinding = new EventBinding<PlayerEvents.Joined>(OnPlayerJoinedEventHandler);
            onPlayerAddedEventBinding = new EventBinding<PlayerEvents.Add>(OnPlayerAddedEventHandler);
            onPlayerRemovedEventBinding = new EventBinding<PlayerEvents.Remove>(OnPlayerRemovedEventHandler);
        }

        /// <summary>
        /// Registers event bindings and subscribes to timer events.
        /// </summary>
        private void OnEnable()
        {
            EventBus<PlayerEvents.Add>.Register(onPlayerAddedEventBinding);
            EventBus<PlayerEvents.Joined>.Register(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Remove>.Register(onPlayerRemovedEventBinding);

            timer.OnUpdate += OnTimerUpdate;
            timer.OnComplete += OnTimerComplete;
        }

        /// <summary>
        /// Deregisters event bindings and unsubscribes from timer events.
        /// </summary>
        private void OnDisable()
        {
            EventBus<PlayerEvents.Add>.Deregister(onPlayerAddedEventBinding);
            EventBus<PlayerEvents.Joined>.Deregister(onPlayerJoinedEventBinding);
            EventBus<PlayerEvents.Remove>.Deregister(onPlayerRemovedEventBinding);

            timer.OnUpdate -= OnTimerUpdate;
            timer.OnComplete -= OnTimerComplete;
        }

#endregion

#region Methods

        /// <summary>
        /// Checks if all players are connected.
        /// </summary>
        /// <returns>True if all players are connected, otherwise false.</returns>
        private bool AllPlayersConnected()
        {
            var playerManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            return addedPlayers.Count >= playerManager.GetActivePlayers().Length;
        }

        /// <summary>
        /// Stops the timer and clears the timer text.
        /// </summary>
        private void StopTimer()
        {
            timer.Deactivate();
            timerText.text = string.Empty;
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when a player is added.
        /// </summary>
        /// <param name="event">The player add event.</param>
        private void OnPlayerAddedEventHandler(PlayerEvents.Add @event)
        {
            if (addedPlayers.Contains(@event.PlayerID))
            {
                return;
            }

            addedPlayers.Add(@event.PlayerID);
            if (AllPlayersConnected() == false)
            {
                return;
            }

            timer.Activate();
        }

        /// <summary>
        /// Handles the event when a player joins.
        /// </summary>
        /// <param name="event">The player joined event.</param>
        private void OnPlayerJoinedEventHandler(PlayerEvents.Joined @event)
        {
            StopTimer();
        }

        /// <summary>
        /// Handles the event when a player is removed.
        /// </summary>
        /// <param name="event">The player remove event.</param>
        private void OnPlayerRemovedEventHandler(PlayerEvents.Remove @event)
        {
            if (addedPlayers.Contains(@event.PlayerID))
            {
                addedPlayers.Remove(@event.PlayerID);
            }

            StopTimer();
            if (addedPlayers.Count == 0 || AllPlayersConnected() == false)
            {
                return;
            }

            timer.Activate();
        }

        /// <summary>
        /// Handles the event when the timer completes.
        /// </summary>
        private void OnTimerComplete()
        {
            timerText.text = "READY!";
            EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
        }

        /// <summary>
        /// Handles the event when the timer updates.
        /// </summary>
        /// <param name="current">The current time.</param>
        /// <param name="duration">The total duration.</param>
        private void OnTimerUpdate(float current, float duration)
        {
            var remaining = Mathf.Max(0, Mathf.FloorToInt(duration - current));

            var output = $"Match starting in {remaining}";
            if (remaining == 0)
            {
                output = "Match starting NOW";
            }

            timerText.text = output;
        }

#endregion
    }
}