namespace GlobalGameJam
{
    public static class ScoreEvents
    {
        public struct Add : IEvent
        {
            public int Value;
        }

        public struct Update : IEvent
        {
            public int Value;
        }
    }
}