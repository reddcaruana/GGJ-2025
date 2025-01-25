using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.Level
{
    public class LevelManager : MonoBehaviour
    {
        public event System.Action OnLevelStart;
        public event System.Action OnLevelStop;
        
        [SerializeField] private PlayerBehavior[] playerBehaviors;

#region Lifecycle Events

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerDataManager.OnPlayerJoined += (id) =>
            {
                playerBehaviors[id].Bind(id);
            };
            
            OnLevelStart?.Invoke();
        }

        private void OnDestroy()
        {
            OnLevelStop?.Invoke();
        }

#endregion
    }
}