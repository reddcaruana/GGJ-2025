using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Animator))]
    internal class PootionListener : MonoBehaviour
    {
        private static readonly int AnimatorExcitedHash = Animator.StringToHash("Excited");
        
        [SerializeField] private IntVariable pootionCountVariable;

        private Animator attachedAnimator;

#region Lifecycle Events

        private void Awake()
        {
            attachedAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            pootionCountVariable.OnValueChanged += OnPootionCountChanged;
        }

        private void OnDisable()
        {
            pootionCountVariable.OnValueChanged -= OnPootionCountChanged;
        }

#endregion

#region Subscriptions

        private void OnPootionCountChanged(int count)
        {
            attachedAnimator.SetBool(AnimatorExcitedHash, count > 0);
        }
        
#endregion
    }
}