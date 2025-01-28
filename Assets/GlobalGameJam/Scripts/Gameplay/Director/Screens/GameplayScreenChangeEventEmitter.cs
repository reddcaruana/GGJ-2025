using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class GameplayScreenChangeEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit
    
        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.ChangeScreens>.Raise(new LevelEvents.ChangeScreens
            {
                Mode = ScreenSetupMode.Gameplay
            });
        }
    
#endregion
    }
}