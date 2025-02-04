namespace GlobalGameJam.Events
{
    public static class DirectorEvents
    {
        /// <summary>
        /// Event structure for the Play event.
        /// </summary>
        public struct Resume : IEvent
        {
            /// <summary>
            /// Gets the default instance of the Play event.
            /// </summary>
            public static Resume Default => new();
        }
    }
}