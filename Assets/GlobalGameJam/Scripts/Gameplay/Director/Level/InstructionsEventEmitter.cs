using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class InstructionsEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit
    
        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.Instructions>.Raise(LevelEvents.Instructions.Default);
        }
    
#endregion
    }
}