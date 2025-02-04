using GlobalGameJam.Events;
using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class ReloadLevelEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit
    
        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.Reload>.Raise(new LevelEvents.Reload
            {
                Delay = 0.1f
            });
        }
    
#endregion
    }
}