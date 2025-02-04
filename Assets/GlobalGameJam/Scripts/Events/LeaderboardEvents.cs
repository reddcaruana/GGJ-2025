namespace GlobalGameJam.Events
{
    public static class LeaderboardEvents
    {
        /// <summary>
        /// Event for adding a score entry to the leaderboard.
        /// </summary>
        public struct Add : IEvent
        {
            /// <summary>
            /// The score entry to be added.
            /// </summary>
            public ScoreEntry Entry;
        }

        /// <summary>
        /// Event for broadcasting the leaderboard.
        /// </summary>
        public struct Broadcast : IEvent
        {
            /// <summary>
            /// Gets the default instance of the Broadcast event.
            /// </summary>
            public static Broadcast Default => new();
        }

        /// <summary>
        /// Event for updating the leaderboard with new entries.
        /// </summary>
        public struct Update : IEvent
        {
            /// <summary>
            /// The array of score entries to update the leaderboard with.
            /// </summary>
            public ScoreEntry[] Entries;
        }
    }
}