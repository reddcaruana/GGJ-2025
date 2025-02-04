using GlobalGameJam.Events;
using GlobalGameJam.Gameplay;
using UnityEngine;

namespace GlobalGameJam.UI
{
    [RequireComponent(typeof(Animator))]
    public class AvatarUI : MonoBehaviour
    {
        private static readonly int AnimatorHappyTrigger = Animator.StringToHash("Happy");
        private static readonly int AnimatorSadTrigger = Animator.StringToHash("Sad");

        private Animator animator;

        private EventBinding<CauldronEvents.EvaluatePotion> onEvaluatePotionEventBinding;

#region Lifecycle Events

        private void Awake()
        {
            animator = GetComponent<Animator>();
            onEvaluatePotionEventBinding = new EventBinding<CauldronEvents.EvaluatePotion>(OnEvaluatePotionEventHandler);
        }

        private void OnEnable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Register(onEvaluatePotionEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CauldronEvents.EvaluatePotion>.Deregister(onEvaluatePotionEventBinding);
        }

#endregion

#region Event Handlers

        private void OnEvaluatePotionEventHandler(CauldronEvents.EvaluatePotion @event)
        {
            if (@event.Outcome is OutcomeType.Success)
            {
                animator.SetTrigger(AnimatorHappyTrigger);
                return;
            }

            if (@event.Outcome is OutcomeType.Failure)
            {
                animator.SetTrigger(AnimatorSadTrigger);
            }
        }

#endregion
    }
}