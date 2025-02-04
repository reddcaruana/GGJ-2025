using GlobalGameJam.Data;

namespace GlobalGameJam.Events
{
    public static class LevelEvents
    {
        /// <summary>
        /// Event for updating the level objective with a new potion.
        /// </summary>
        public struct ObjectiveUpdated : IEvent
        {
            /// <summary>
            /// The potion data for the updated objective.
            /// </summary>
            public PotionData Potion;
        }

        /// <summary>
        /// Event for reloading the level with a specified delay.
        /// </summary>
        public struct Reload : IEvent
        {
            /// <summary>
            /// The delay before reloading the level.
            /// </summary>
            public float Delay;
        }

        /// <summary>
        /// Event for setting the monitor mode.
        /// </summary>
        public struct SetMonitors : IEvent
        {
            /// <summary>
            /// The monitor mode to be set.
            /// </summary>
            public MonitorMode Mode;
        }

        /// <summary>
        /// Event for setting the level mode.
        /// </summary>
        public struct SetMode : IEvent
        {
            /// <summary>
            /// The level mode to be set.
            /// </summary>
            public LevelMode Mode;
        }
    }
}