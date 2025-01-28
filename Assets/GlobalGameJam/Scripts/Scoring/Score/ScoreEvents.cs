namespace GlobalGameJam
{
    public static class ScoreEvents
    {
        public struct Add : IEvent
        {
            public int Value;
        }

        public struct SetInitial : IEvent
        {
            public int PlayerID;
            public char Initial;
        }

        public struct Update : IEvent
        {
            public int Value;
        }
        
        public struct Submit : IEvent
        {
            public static Submit Default = new();
        }
    }
}