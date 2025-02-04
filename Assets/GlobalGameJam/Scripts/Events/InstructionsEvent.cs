namespace GlobalGameJam.Events
{
    public static class InstructionsEvent
    {
        public struct Navigate : IEvent
        {
            public NavigationMode Navigation;
        }
    }
}