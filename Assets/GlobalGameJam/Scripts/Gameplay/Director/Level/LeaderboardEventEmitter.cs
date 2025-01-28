using UnityEngine;
using UnityEngine.Timeline;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(SignalReceiver))]
    public class LeaderboardEventEmitter : MonoBehaviour, IEmit
    {
#region Implementation of IEmit
    
        /// <inheritdoc />
        public void Emit()
        {
            EventBus<LevelEvents.Leaderboard>.Raise(LevelEvents.Leaderboard.Default);
        }
    
#endregion
    }
}