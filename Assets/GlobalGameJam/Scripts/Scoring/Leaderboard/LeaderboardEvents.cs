namespace GlobalGameJam
{
    public static class LeaderboardEvents
    {
        public struct Add : IEvent
        {
            public ScoreEntry Entry;
        }

        public struct Broadcast : IEvent
        {
            public static Broadcast Default => new();
        }

        public struct Update : IEvent
        {
            public ScoreEntry[] Entries;
        }
    }
}