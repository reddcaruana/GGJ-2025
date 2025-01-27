using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class CauldronIngredient : MonoBehaviour
    {
        private static readonly int AnimatorIsFloatingBool = Animator.StringToHash("IsFloating");
        private static readonly int AnimatorAnimationOffsetFloat = Animator.StringToHash("AnimationOffset");

        private SpriteRenderer spriteRenderer;
        private Animator animator;

#region Lifecycle Events

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            transform.eulerAngles = Vector3.left * 90.0f + Vector3.forward * Random.value * 360.0f;
        }

        private void Update()
        {
            var rotation = transform.eulerAngles;
            rotation.z += 5.0f * Time.deltaTime;
            transform.eulerAngles = rotation;
        }

#endregion
        
#region Methods

        public void Emerge(IngredientData ingredient)
        {
            spriteRenderer.sprite = ingredient.Sprite;
            animator.SetBool(AnimatorIsFloatingBool, true);
        }

        public void Submerge()
        {
            animator.SetBool(AnimatorIsFloatingBool, false);
        }

        public void SetAnimationOffset(float offsetValue)
        {
            animator.SetFloat(AnimatorAnimationOffsetFloat, offsetValue);
        }

#endregion
    }
}