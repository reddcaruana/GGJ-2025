using GlobalGameJam.Events;
using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class LevelStartEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit
    
        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.SetMode>.Raise(new LevelEvents.SetMode
            {
                Mode = LevelMode.Start
            });
        }
    
#endregion
    }
}