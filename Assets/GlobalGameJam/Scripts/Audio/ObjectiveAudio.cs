using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.Audio
{
    /// <summary>
    /// Manages the audio playback for objective-related events, such as potion evaluation outcomes.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class ObjectiveAudio : MonoBehaviour
    {
        /// <summary>
        /// Audio clip to play on successful potion evaluation.
        /// </summary>
        [SerializeField] private AudioClip successClip;

        /// <summary>
        /// Audio clip to play on failed potion evaluation.
        /// </summary>
        [SerializeField] private AudioClip failureClip;

        /// <summary>
        /// The AudioSource component used to play audio clips.
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// Event binding for the EvaluatePotion event.
        /// </summary>
        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the AudioSource component and event binding for the EvaluatePotion event.
        /// </summary>
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        /// <summary>
        /// Registers the EvaluatePotion event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        /// <summary>
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
        /// Plays the appropriate audio clip based on the outcome of the potion evaluation.
        /// </summary>
        /// <param name="event">The EvaluatePotion event.</param>
        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            switch (@event.Outcome)
            {
                case OutcomeType.Success:
                    if (successClip)
                    {
                        audioSource.PlayOneShot(successClip);
                    }
                    break;

                case OutcomeType.Failure:
                    if (failureClip)
                    {
                        audioSource.PlayOneShot(failureClip);
                    }
                    break;
            }
        }

#endregion
    }
}