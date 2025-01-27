using UnityEngine.InputSystem;

namespace GlobalGameJam.Players
{
    public static class PlayerEvents
    {
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
    }
}