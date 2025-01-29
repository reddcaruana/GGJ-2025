using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GlobalGameJam.UI
{
    /// <summary>
    /// Manages the UI for displaying the leaderboard.
    /// </summary>
    public class LeaderboardUI : MonoBehaviour
    {
        /// <summary>
        /// The prefab for the leaderboard entry UI.
        /// </summary>
        [SerializeField] private LeaderboardEntryUI entryUIPrefab;

        /// <summary>
        /// The object pool for reusing leaderboard entry UI instances.
        /// </summary>
        private ObjectPool<LeaderboardEntryUI> entryPool;

        /// <summary>
        /// The list of active leaderboard entry UI instances.
        /// </summary>
        private readonly List<LeaderboardEntryUI> entries = new();

        /// <summary>
        /// Event binding for handling leaderboard update events.
        /// </summary>
        private EventBinding<LeaderboardEvents.Update> onLeaderboardUpdateEventBinding;

        /// <summary>
        /// Indicates whether an update has been requested.
        /// </summary>
        private static bool hasRequestedUpdate;

#region Lifecycle Events

        /// <summary>
        /// Initializes the object pool and event binding.
        /// </summary>
        private void Awake()
        {
            entryPool = new ObjectPool<LeaderboardEntryUI>(
                createFunc: CreateEntry,
                actionOnGet: GetEntry,
                actionOnRelease: ReleaseEntry);

            onLeaderboardUpdateEventBinding = new EventBinding<LeaderboardEvents.Update>(OnLeaderboardUpdateEventHandler);
        }

        /// <summary>
        /// Registers the event binding for leaderboard updates.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LeaderboardEvents.Update>.Register(onLeaderboardUpdateEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding for leaderboard updates.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LeaderboardEvents.Update>.Deregister(onLeaderboardUpdateEventBinding);
        }

        /// <summary>
        /// Raises a broadcast event if an update has not been requested.
        /// </summary>
        private void Start()
        {
            if (hasRequestedUpdate == false)
            {
                EventBus<LeaderboardEvents.Broadcast>.Raise(LeaderboardEvents.Broadcast.Default);
            }
        }

#endregion

#region Methods

        /// <summary>
        /// Clears all active leaderboard entry UI instances.
        /// </summary>
        private void Clear()
        {
            foreach (var entry in entries)
            {
                entryPool.Release(entry);
            }

            entries.Clear();
        }

        /// <summary>
        /// Updates the leaderboard UI with the given score entries.
        /// </summary>
        /// <param name="scoreEntries">The array of score entries to display.</param>
        private void UpdateEntries(ScoreEntry[] scoreEntries)
        {
            Clear();
            foreach (var entry in scoreEntries)
            {
                var item = entryPool.Get();
                item.SetData(entry);

                entries.Add(item);
            }
        }

#endregion

#region ObjectPool Implementations

        /// <summary>
        /// Creates a new instance of the leaderboard entry UI.
        /// </summary>
        /// <returns>A new instance of <see cref="LeaderboardEntryUI"/>.</returns>
        private LeaderboardEntryUI CreateEntry()
        {
            var instance = Instantiate(entryUIPrefab, transform);
            instance.gameObject.SetActive(false);

            return instance;
        }

        /// <summary>
        /// Releases the specified leaderboard entry UI instance back to the pool.
        /// </summary>
        /// <param name="instance">The instance to release.</param>
        private void ReleaseEntry(LeaderboardEntryUI instance)
        {
            instance.gameObject.SetActive(false);
        }

        /// <summary>
        /// Gets a leaderboard entry UI instance from the pool.
        /// </summary>
        /// <param name="instance">The instance to get.</param>
        private void GetEntry(LeaderboardEntryUI instance)
        {
            instance.transform.SetAsLastSibling();
            instance.gameObject.SetActive(true);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the leaderboard update event.
        /// </summary>
        /// <param name="event">The event containing the updated leaderboard entries.</param>
        private void OnLeaderboardUpdateEventHandler(LeaderboardEvents.Update @event)
        {
            UpdateEntries(@event.Entries);
        }

#endregion
    }
}