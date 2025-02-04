namespace GlobalGameJam.Events
{
    public static class ScoreEvents
    {
        public struct Add : IEvent
        {
            public int Potions;
            public int Litter;
            
            public int Earnings;
            public int Deductions;
        }

        public struct SetInitial : IEvent
        {
            public int PlayerID;
            public char Initial;
        }

        public struct Update : IEvent
        {
            public int PotionCount;
            public int LitterCount;
            public int Earnings;
        }
        
        public struct Submit : IEvent
        {
            public static Submit Default = new();
        }
    }
}