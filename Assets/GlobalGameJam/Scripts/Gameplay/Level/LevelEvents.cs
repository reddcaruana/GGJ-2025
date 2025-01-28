using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public static class LevelEvents
    {
        public struct ChangeScreens : IEvent
        {
            public ScreenSetupMode Mode;
        }

        public struct End : IEvent
        {
            public static End Default => new();
        }
        
        public struct Instructions : IEvent
        {
            public static Instructions Default => new();
        }

        public struct Leaderboard : IEvent
        {
            public static Leaderboard Default => new();
        }

        public struct ObjectiveUpdated : IEvent
        {
            public PotionData Potion;
        }
        
        public struct Start : IEvent
        {
            public static Start Default => new();
        }
    }
}