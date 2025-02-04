using GlobalGameJam.Events;
using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class ScreenChangeEventEmitter : MonoBehaviour, IEmit
    {
        [SerializeField] private MonitorMode mode;
        
#region Implementation of IEmit

        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.SetMonitors>.Raise(new LevelEvents.SetMonitors
            {
                Mode = mode
            });
        }

#endregion
    }
}