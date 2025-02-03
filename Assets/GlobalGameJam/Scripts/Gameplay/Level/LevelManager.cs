using System.Collections.Generic;
using GlobalGameJam.Data;
using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the level lifecycle, player events, and chest batches.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// Array of player behaviors in the level.
        /// </summary>
        [SerializeField] private PlayerBehavior[] playerBehaviors;

        /// <summary>
        /// Array of chest batches in the level.
        /// </summary>
        [SerializeField] private ChestBatch[] chestBatches;

        /// <summary>
        /// Timer for the start of the level.
        /// </summary>
        [SerializeField] private Timer startTimer;

        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;
        private EventBinding<LevelEvents.End> onLevelEndEventBinding;

        private EventBinding<PlayerEvents.Add> onAddPlayerEventBinding;
        private EventBinding<PlayerEvents.Remove> onRemovePlayerEventBinding;

        private readonly List<int> joinedPlayers = new();

#region Lifecycle Events

        /// <summary>
        /// Initializes event bindings.
        /// </summary>
        private void Awake()
        {
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
            onLevelEndEventBinding = new EventBinding<LevelEvents.End>(OnLevelEndEventHandler);

            onAddPlayerEventBinding = new EventBinding<PlayerEvents.Add>(OnAddPlayerEventHandler);
            onRemovePlayerEventBinding = new EventBinding<PlayerEvents.Remove>(OnRemovePlayerEventHandler);
        }

        /// <summary>
        /// Registers event bindings.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Register(onLevelEndEventBinding);

            EventBus<PlayerEvents.Add>.Register(onAddPlayerEventBinding);
            EventBus<PlayerEvents.Remove>.Register(onRemovePlayerEventBinding);
        }

        /// <summary>
        /// Deregisters event bindings.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
            EventBus<LevelEvents.End>.Deregister(onLevelEndEventBinding);

            EventBus<PlayerEvents.Add>.Deregister(onAddPlayerEventBinding);
            EventBus<PlayerEvents.Remove>.Deregister(onRemovePlayerEventBinding);
        }

        /// <summary>
        /// Resets the player behaviors array.
        /// </summary>
        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        /// <summary>
        /// Initializes the level by activating a random chest batch and setting its chests.
        /// </summary>
        private void Start()
        {
            var ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            var randomIndex = Random.Range(0, chestBatches.Length);
            chestBatches[randomIndex].gameObject.SetActive(true);
            chestBatches[randomIndex].SetChests(ingredientRegistry.Ingredients);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the event when a player is added.
        /// </summary>
        /// <param name="event">The player add event.</param>
        private void OnAddPlayerEventHandler(PlayerEvents.Add @event)
        {
            if (joinedPlayers.Contains(@event.PlayerID))
            {
                return;
            }

            var position = playerBehaviors[@event.PlayerID].transform.position;
            position.y = 0;
            playerBehaviors[@event.PlayerID].transform.position = position;

            joinedPlayers.Add(@event.PlayerID);
        }

        /// <summary>
        /// Handles the event when a player is removed.
        /// </summary>
        /// <param name="event">The player remove event.</param>
        private void OnRemovePlayerEventHandler(PlayerEvents.Remove @event)
        {
            if (joinedPlayers.Contains(@event.PlayerID) == false)
            {
                return;
            }

            var position = playerBehaviors[@event.PlayerID].transform.position;
            position.y = -10;
            playerBehaviors[@event.PlayerID].transform.position = position;

            joinedPlayers.Remove(@event.PlayerID);
        }

        /// <summary>
        /// Handles the event when the level starts.
        /// </summary>
        /// <param name="event">The level start event.</param>
        private void OnLevelStartEventHandler(LevelEvents.Start @event)
        {
            for (var i = 0; i < playerBehaviors.Length; i++)
            {
                playerBehaviors[i].Bind(i);
            }
        }

        /// <summary>
        /// Handles the event when the level ends.
        /// </summary>
        /// <param name="event">The level end event.</param>
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