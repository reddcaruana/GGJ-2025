using System.Collections.Generic;
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

        private List<int> joinedPlayers = new();
        
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
            var position = playerBehaviors[@event.PlayerID].transform.position;
            position.y = 0;
            playerBehaviors[@event.PlayerID].transform.position = position;

            if (joinedPlayers.Contains(@event.PlayerID) == false)
            {
                joinedPlayers.Add(@event.PlayerID);
            }
            
            var playerManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            if (joinedPlayers.Count >= playerManager.GetActivePlayers().Length)
            {
                EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
            }
        }

        private void OnRemovePlayerEventHandler(PlayerEvents.Remove @event)
        {
            var position = playerBehaviors[@event.PlayerID].transform.position;
            position.y = -10;
            playerBehaviors[@event.PlayerID].transform.position = position;

            if (joinedPlayers.Contains(@event.PlayerID))
            {
                joinedPlayers.Remove(@event.PlayerID);
            }
        }
        
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