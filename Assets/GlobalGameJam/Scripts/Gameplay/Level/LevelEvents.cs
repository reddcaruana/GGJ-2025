using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public static class LevelEvents
    {
        public struct AddScore : IEvent
        {
            public int Value;
        }
    
        public struct Start : IEvent
        {
            public static Start Default => new();
        }

        public struct End : IEvent
        {
            public static End Default => new();
        }

        public struct ObjectiveUpdated : IEvent
        {
            public PotionData Potion;
        }

        public struct TimerUpdate : IEvent
        {
            public float Remaining;
        }
    }
}