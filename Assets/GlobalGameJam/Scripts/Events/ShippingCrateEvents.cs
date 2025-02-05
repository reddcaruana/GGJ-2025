using GlobalGameJam.Data;

namespace GlobalGameJam.Events
{
    public static class ShippingCrateEvents
    {
        /// <summary>
        /// Event data for adding a potion to the shipping crate.
        /// </summary>
        public struct Add : IEvent
        {
            /// <summary>
            /// The potion data associated with the event.
            /// </summary>
            public PotionData Potion;
        }
    }
}