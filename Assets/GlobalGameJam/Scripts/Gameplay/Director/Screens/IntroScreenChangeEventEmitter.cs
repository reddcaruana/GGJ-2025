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
            EventBus<LevelEvents.ChangeScreens>.Raise(new LevelEvents.ChangeScreens
            {
                Mode = ScreenSetupMode.Intro
            });
        }

#endregion
    }
}