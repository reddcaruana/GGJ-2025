using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class CauldronIngredient : MonoBehaviour
    {
        private static readonly int AnimatorIsFloatingBool = Animator.StringToHash("IsFloating");
        private static readonly int AnimatorAnimationOffsetFloat = Animator.StringToHash("AnimationOffset");

        [Header("Rotation")] [SerializeField] private float angularVelocity = 5.0f;

        [Header("Motion")] [SerializeField] private float radius = 0.1f;
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private Vector2 noiseOffset;

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private Vector3 anchorPosition;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the sprite renderer and animator components.
        /// </summary>
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Sets the initial rotation and anchor position.
        /// </summary>
        private void Start()
        {
            transform.eulerAngles = Vector3.left * 90.0f + Vector3.forward * Random.value * 360.0f;
            anchorPosition = transform.localPosition;
        }

        /// <summary>
        /// Called every frame.
        /// Updates the position and rotation of the object.
        /// </summary>
        private void Update()
        {
            Move(Time.time);
            Rotate(Time.deltaTime);
        }

#endregion

#region Methods

        /// <summary>
        /// Sets the ingredient sprite and starts the floating animation.
        /// </summary>
        /// <param name="ingredient">The ingredient data.</param>
        public void Emerge(IngredientData ingredient)
        {
            spriteRenderer.sprite = ingredient.Sprite;
            animator.SetBool(AnimatorIsFloatingBool, true);
        }

        /// <summary>
        /// Moves the object in a smooth random motion using Perlin noise.
        /// </summary>
        /// <param name="time">The current time.</param>
        private void Move(float time)
        {
            var x = Mathf.PerlinNoise((time + noiseOffset.x) * speed, 0) * 2 - 1;
            var y = Mathf.PerlinNoise((time + noiseOffset.y) * speed, 0) * 2 - 1;
            var offset = (Vector3.right * x + Vector3.forward * y) * radius;

            transform.localPosition = anchorPosition + offset;
        }

        /// <summary>
        /// Rotates the object around the z-axis.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
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