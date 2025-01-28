using UnityEngine;
using UnityEngine.UI;

namespace GlobalGameJam.UI
{
    [RequireComponent(typeof(Animator))]
    public class ObjectiveItemUI : MonoBehaviour
    {
        /// <summary>
        /// Hash for the "IsActive" boolean parameter in the animator.
        /// </summary>
        private static readonly int AnimatorIsActiveBool = Animator.StringToHash("IsActive");

        /// <summary>
        /// The image component to display the objective item.
        /// </summary>
        [SerializeField] private Image image;
        
        /// <summary>
        /// The animator component for handling animations.
        /// </summary>
        private Animator animator;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the animator component and sets the "IsActive" parameter to false.
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetBool(AnimatorIsActiveBool, false);
        }

#endregion

#region Methods

        /// <summary>
        /// Clears the current sprite by setting it to null.
        /// </summary>
        public void ClearSprite()
        {
            SetSprite(null);
        }
        
        /// <summary>
        /// Sets the sprite to the specified one and updates the "IsActive" parameter in the animator.
        /// </summary>
        /// <param name="sprite">The new image to set.</param>
        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
            image.preserveAspect = true;
            
            animator.SetBool(AnimatorIsActiveBool, sprite is not null);
        }

#endregion
    }
}