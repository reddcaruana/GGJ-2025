using UnityEngine.InputSystem;
using WitchesBasement.Events;

namespace WitchesBasement.Players
{
    public struct PlayerStatusChangedEvent : IEvent
    {
        public enum StatusType { Joined, Ready, Left }

        public StatusType Status;
        public int PlayerID;
        public PlayerInput PlayerInput;
    }
}