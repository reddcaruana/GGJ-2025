using System.Collections.Generic;
using GlobalGameJam.Data;
using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Updates the objectives in the game by managing the target potion and handling relevant events.
    /// </summary>
    public class ObjectiveUpdater : MonoBehaviour
    {
        /// <summary>
        /// List of available potions.
        /// </summary>
        private readonly List<PotionData> potions = new();

        /// <summary>
        /// The current target potion.
        /// </summary>
        private PotionData target;

        /// <summary>
        /// Event binding for the evaluate potion event.
        /// </summary>
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

        /// <summary>
        /// Event binding for the level start event.
        /// </summary>
        private EventBinding<LevelEvents.SetMode> onSetLevelModeEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the objective updater by setting up the potion list and event bindings.
        /// </summary>
        private void Awake()
        {
            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();

            potions.Clear();
            potions.AddRange(potionRegistry.Potions);

            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
            onSetLevelModeEventBinding = new EventBinding<LevelEvents.SetMode>(OnSetLevelModeEventHandler);
        }

        /// <summary>
        /// Registers the event bindings when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
            EventBus<LevelEvents.SetMode>.Register(onSetLevelModeEventBinding);
        }

        /// <summary>
        /// Deregisters the event bindings when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
            EventBus<LevelEvents.SetMode>.Deregister(onSetLevelModeEventBinding);
        }

#endregion

#region Methods

        /// <summary>
        /// Selects the next target potion randomly from the list of available potions.
        /// </summary>
        private void Next()
        {
            var index = Random.Range(0, potions.Count);
            var next = potions[index];

            if (target is not null)
            {
                potions.Add(target);
            }

            potions.Remove(next);

            target = next;
            EventBus<LevelEvents.ObjectiveUpdated>.Raise(new LevelEvents.ObjectiveUpdated
            {
                Potion = target
            });
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the evaluate potion event by selecting the next target potion if the outcome is successful.
        /// </summary>
        /// <param name="event">The evaluate potion event data.</param>
        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            if (@event.Outcome is not OutcomeType.Success)
            {
                return;
            }

            Next();
        }

        /// <summary>
        /// Handles the level start event by selecting the next target potion.
        /// </summary>
        /// <param name="event">The level start event data.</param>
        private void OnSetLevelModeEventHandler(LevelEvents.SetMode @event)
        {
            if (@event.Mode is not LevelMode.Start)
            {
                return;
            }
            
            Next();
        }

#endregion
    }
}