using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Animator))]
    internal class TrashReceiver : ItemReceiverBase<ItemData>
    {
        private static readonly int AnimatorEatHash = Animator.StringToHash("Eat");

        [SerializeField] private SpriteRenderer contentsSpriteRenderer;

        [SerializeField] private IntVariable pootionCountVariable;

        private PotionData pootionData;
        private Animator attachedAnimator;

#region Lifecycle Events
        
        private void Awake()
        {
            attachedAnimator = GetComponent<Animator>();

            var registry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            pootionData = registry.Pootion;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Validate(other, out var item, out var data) == false)
            {
                return;
            }

            if (data == pootionData)
            {
                pootionCountVariable.Value -= 1;
            }
            
            item.Use();
            contentsSpriteRenderer.sprite = data.Sprite;
            attachedAnimator.SetTrigger(AnimatorEatHash);
        }

#endregion
    }
}