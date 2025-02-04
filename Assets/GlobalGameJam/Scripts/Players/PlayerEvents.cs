using UnityEngine.InputSystem;

namespace GlobalGameJam.Players
{
    public static class PlayerEvents
    {
        public struct Add : IEvent
        {
            public int PlayerID;
        }
        
        public struct Joined : IEvent
        {
            public int PlayerID;
            public PlayerInput PlayerInput;
        }

        public struct Left : IEvent
        {
            public int PlayerID;
            public PlayerInput PlayerInput;
        }

        public struct Remove : IEvent
        {
            public int PlayerID;
        }

        public struct EnableJoining : IEvent
        {
            public static EnableJoining Default => new();
        }
    }
}