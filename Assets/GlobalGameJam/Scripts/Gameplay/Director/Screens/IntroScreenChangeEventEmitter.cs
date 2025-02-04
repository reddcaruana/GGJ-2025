using GlobalGameJam.Events;
using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class IntroScreenChangeEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit

        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.SetMonitors>.Raise(new LevelEvents.SetMonitors
            {
                Mode = MonitorMode.Intro
            });
        }

#endregion
    }
}