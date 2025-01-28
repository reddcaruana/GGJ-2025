using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the particle effects for the cauldron based on potion evaluation outcomes.
    /// </summary>
    public class CauldronParticles : MonoBehaviour
    {
        /// <summary>
        /// Particle system to play on successful potion evaluation.
        /// </summary>
        [SerializeField] private ParticleSystem successParticles;

        /// <summary>
        /// Particle system to play on failed potion evaluation.
        /// </summary>
        [SerializeField] private ParticleSystem failureParticles;

        /// <summary>
        /// Event binding for the EvaluatePotion event.
        /// </summary>
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the EvaluatePotion event.
        /// </summary>
        private void Awake()
        {
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the EvaluatePotion event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the EvaluatePotion event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the EvaluatePotion event.
        /// Plays the appropriate particle system based on the outcome of the potion evaluation.
        /// </summary>
        /// <param name="event">The EvaluatePotion event.</param>
        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            switch (@event.Outcome)
            {
                case OutcomeType.Success:
                    successParticles.Play();
                    break;

                case OutcomeType.Failure:
                    failureParticles.Play();
                    break;
            }
        }

#endregion
    }
}