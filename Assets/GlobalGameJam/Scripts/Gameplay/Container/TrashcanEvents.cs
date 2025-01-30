namespace GlobalGameJam.Gameplay
{
    public static class TrashcanEvents
    {
        public struct Excite : IEvent
        {
            public static Excite Default => new();
        }
    }
}