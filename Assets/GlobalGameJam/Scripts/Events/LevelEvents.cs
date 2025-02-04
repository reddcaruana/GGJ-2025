using GlobalGameJam.Data;

namespace GlobalGameJam.Events
{
    public static class LevelEvents
    {
        public struct SetMonitors : IEvent
        {
            public MonitorMode Mode;
        }

        public struct SetMode : IEvent
        {
            public LevelMode Mode;
        }

        public struct ObjectiveUpdated : IEvent
        {
            public PotionData Potion;
        }
    }
}