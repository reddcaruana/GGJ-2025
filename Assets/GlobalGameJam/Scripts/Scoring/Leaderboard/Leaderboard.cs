using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalGameJam
{
    /// <summary>
    /// Manages the leaderboard.
    /// </summary>
    public class Leaderboard : MonoBehaviour
    {
        /// <summary>
        /// Gets the list of score entries on the leaderboard.
        /// </summary>
        public List<ScoreEntry> Entries { get; private set; } = new();

        /// <summary>
        /// Event binding for handling the addition of new entries to the leaderboard.
        /// </summary>
        private EventBinding<LeaderboardEvents.Add> onAddEntryEventBinding;

        /// <summary>
        /// Event binding for broadcasting an update.
        /// </summary>
        private EventBinding<LeaderboardEvents.Broadcast> onBroadcastEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for adding entries.
        /// </summary>
        private void Awake()
        {
            onAddEntryEventBinding = new EventBinding<LeaderboardEvents.Add>(OnAddEntryEventHandler);
            onBroadcastEventBinding = new EventBinding<LeaderboardEvents.Broadcast>(OnBroadcastEventHandler);

            var scores = FileUtility.LoadScores();
            Entries = new List<ScoreEntry>(scores);
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// Registers the event binding for adding entries.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LeaderboardEvents.Add>.Register(onAddEntryEventBinding);
            EventBus<LeaderboardEvents.Broadcast>.Register(onBroadcastEventBinding);
        }

        /// <summary>
        /// Called when the object becomes disabled or inactive.
        /// Deregisters the event binding for adding entries.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LeaderboardEvents.Add>.Deregister(onAddEntryEventBinding);
            EventBus<LeaderboardEvents.Broadcast>.Deregister(onBroadcastEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event of adding a new entry to the leaderboard.
        /// Adds the entry, sorts the list by score, and raises an update event.
        /// </summary>
        /// <param name="event">The event containing the new score entry.</param>
        private void OnAddEntryEventHandler(LeaderboardEvents.Add @event)
        {
            Entries.Add(@event.Entry);
            Entries = Entries.OrderBy(entry => entry.Score).ToList();
            
            FileUtility.SaveScores(Entries.ToArray());
            
            EventBus<LeaderboardEvents.Broadcast>.Raise(LeaderboardEvents.Broadcast.Default);
        }

        /// <summary>
        /// Handles the broadcast event.
        /// Raises an update event with the current leaderboard entries.
        /// </summary>
        /// <param name="event">The broadcast event.</param>
        private void OnBroadcastEventHandler(LeaderboardEvents.Broadcast @event)
        {
            EventBus<LeaderboardEvents.Update>.Raise(new LeaderboardEvents.Update
            {
                Entries = Entries.ToArray()
            });
        }

#endregion
    }
}