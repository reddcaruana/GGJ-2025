using UnityEngine;
using UnityEngine.UI;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Animator))]
    internal class ObjectiveMonitorTarget : MonoBehaviour
    {
        private static readonly int AnimatorIsActiveBool = Animator.StringToHash("IsActive");

        [SerializeField] private Image image;

        private Animator animator;

#region Lifecycle Events

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Clear();
        }

#endregion

#region Methods

        public void Clear()
        {
            Set(null);
        }

        public void Set(Sprite sprite)
        {
            image.sprite = sprite;
            image.preserveAspect = true;
            
            animator.SetBool(AnimatorIsActiveBool, sprite is not null);
        }

#endregion
    }
}