using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class IngredientVisual : MonoBehaviour
    {
        private static readonly int AnimatorIsFloatingBool = Animator.StringToHash("IsFloating");
        private static readonly int AnimatorAnimationOffsetFloat = Animator.StringToHash("AnimationOffset");

        [SerializeField] private float angularVelocity = 5.0f;
        [SerializeField] private float moveRadius = 0.2f;
        [SerializeField] private float moveSpeed = 0.1f;
        [SerializeField] private Vector2 offsetNoise = Vector2.one * 0.2f;

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private Vector3 anchorPoint;

#region Lifecycle Events

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            anchorPoint = transform.localPosition;
            transform.rotation = Quaternion.Euler(Vector3.left * 90.0f + Vector3.forward * Random.value * 360.0f);
        }

        private void Update()
        {
            Move(Time.time);
            Rotate(Time.deltaTime);
        }

#endregion

#region Methods
        
        public void Emerge(IngredientData ingredient)
        {
            spriteRenderer.sprite = ingredient.Sprite;
            animator.SetBool(AnimatorIsFloatingBool, true);
        }

        private void Move(float time)
        {
            var x = Mathf.PerlinNoise((time + offsetNoise.x) * moveSpeed, 0) * 2 - 1;
            var y = Mathf.PerlinNoise((time + offsetNoise.y) * moveSpeed, 0) * 2 - 1;
            var offset = (Vector3.right * x + Vector3.forward * y) * moveRadius;

            transform.localPosition = anchorPoint + offset;
        }

        private void Rotate(float deltaTime)
        {
            var rotation = transform.eulerAngles;
            rotation.z += angularVelocity * deltaTime;
            transform.eulerAngles = rotation;
        }
        
        /// <summary>
        /// Stops the floating animation.
        /// </summary>
        public void Submerge()
        {
            animator.SetBool(AnimatorIsFloatingBool, false);
        }
        
        /// <summary>
        /// Sets the animation offset for the floating animation.
        /// </summary>
        /// <param name="offsetValue">The offset value.</param>
        public void SetAnimationOffset(float offsetValue)
        {
            animator.SetFloat(AnimatorAnimationOffsetFloat, offsetValue);
        }

#endregion
    }
}