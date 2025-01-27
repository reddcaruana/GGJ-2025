using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public static class LevelEvents
    {
        public struct Start : IEvent
        {
            public static Start Default => new();
        }

        public struct ObjectiveUpdated : IEvent
        {
            public PotionData Potion;
        }

        public struct End : IEvent
        {
            public static End Default => new();
        }
    }
}