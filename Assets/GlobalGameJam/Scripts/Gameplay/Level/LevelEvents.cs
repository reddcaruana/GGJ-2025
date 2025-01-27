namespace GlobalGameJam.Gameplay
{
    public static class LevelEvents
    {
        public struct Start : IEvent
        { }

        public struct ObjectiveUpdated : IEvent
        {
            
        }
        
        public struct End : IEvent
        { }
    }
}