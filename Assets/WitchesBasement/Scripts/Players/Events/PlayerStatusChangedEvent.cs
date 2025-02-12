using UnityEngine.InputSystem;
using WitchesBasement.Events;

namespace WitchesBasement.Players
{
    public struct PlayerStatusChangedEvent : IEvent
    {
        public enum StatusType { Joined, Added, Removed, Left }

        public StatusType Status;
        public int PlayerID;
        public PlayerInput PlayerInput;
    }
}