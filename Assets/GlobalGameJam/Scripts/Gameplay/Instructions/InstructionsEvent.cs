namespace GlobalGameJam.Gameplay
{
    public static class InstructionsEvent
    {
        public struct Back : IEvent
        {
            public static Back Default => new();
        }
        
        public struct Next : IEvent
        {
            public static Next Default => new();
        }
    }
}