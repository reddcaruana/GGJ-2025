using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Handles the spawning and throwing of potions when a concocted potion event is triggered.
    /// </summary>
    public class PotionSpawner : MonoBehaviour
    {
        /// <summary>
        /// The force with which the potion is thrown.
        /// </summary>
        [SerializeField] private float force = 10.0f;

        /// <summary>
        /// The angle at which the potion is thrown.
        /// </summary>
        [SerializeField] private float angle = 45.0f;

        /// <summary>
        /// The direction in which the potion is thrown.
        /// </summary>
        [SerializeField] private Direction direction = Direction.South;

        /// <summary>
        /// Event binding for the concocted potion event.
        /// </summary>
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the concocted potion event.
        /// </summary>
        private void Awake()
        {
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the concocted potion event by generating and throwing a potion.
        /// </summary>
        /// <param name="event">The concocted potion event data.</param>
        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            if (@event.Outcome is OutcomeType.None)
            {
                return;
            }
            
            var potionManager = Singleton.GetOrCreateMonoBehaviour<PotionPool>();

            var potion = potionManager.Generate(@event.Potion, transform);
            potion.Throw(direction.ToVector(), force, angle);
        }

#endregion
    }
}