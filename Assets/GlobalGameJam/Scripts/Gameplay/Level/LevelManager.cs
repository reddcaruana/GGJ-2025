using GlobalGameJam.Data;
using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerBehavior[] playerBehaviors;
        [SerializeField] private ChestBatch[] chestBatches;

        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;
        private EventBinding<LevelEvents.End> onLevelEndEventBinding;

        private EventBinding<PlayerEvents.Add> onAddPlayerEventBinding;
        private EventBinding<PlayerEvents.Remove> onRemovePlayerEventBinding;
        
#region Lifecycle Events

        private void Awake()
        {
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
            onLevelEndEventBinding = new EventBinding<LevelEvents.End>(OnLevelEndEventHandler);

            onAddPlayerEventBinding = new EventBinding<PlayerEvents.Add>(OnAddPlayerEventHandler);
            onRemovePlayerEventBinding = new EventBinding<PlayerEvents.Remove>(OnRemovePlayerEventHandler);
        }

        private void OnEnable()
        {
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Register(onLevelEndEventBinding);
            
            EventBus<PlayerEvents.Add>.Register(onAddPlayerEventBinding);
            EventBus<PlayerEvents.Remove>.Register(onRemovePlayerEventBinding);
        }

        private void OnDisable()
        {
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Deregister(onLevelEndEventBinding);
            
            EventBus<PlayerEvents.Add>.Deregister(onAddPlayerEventBinding);
            EventBus<PlayerEvents.Remove>.Deregister(onRemovePlayerEventBinding);
        }

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            var ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            var randomIndex = Random.Range(0, chestBatches.Length);
            chestBatches[randomIndex].gameObject.SetActive(true);
            chestBatches[randomIndex].SetChests(ingredientRegistry.Ingredients);
        }

#endregion

#region Event Handlers

        private void OnAddPlayerEventHandler(PlayerEvents.Add @event)
        {
            if (@event.PlayerID >= 0)
            {
                EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
            }
        }

        private void OnRemovePlayerEventHandler(PlayerEvents.Remove @event)
        { }
        
        private void OnLevelStartEventHandler(LevelEvents.Start @event)
        {
            for (var i = 0; i < playerBehaviors.Length; i++)
            {
                playerBehaviors[i].Bind(i);
            }
        }

        private void OnLevelEndEventHandler(LevelEvents.End @event)
        {
            foreach (var playerBehavior in playerBehaviors)
            {
                playerBehavior.Release();
            }
        }

#endregion
    }
}