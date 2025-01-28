namespace GlobalGameJam.Gameplay
{
    public static class TimerEvents
    {
        public struct Extend : IEvent
        {
            public float Duration;
        }
        
        public struct Update : IEvent
        {
            public float Remaining;
        }
    }
}