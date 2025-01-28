using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents an abstract base class for carryable objects in the game.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Carryable : MonoBehaviour
    {
        /// <summary>
        /// The sprite renderer for the carryable object.
        /// </summary>
        [SerializeField] protected SpriteRenderer spriteRenderer;

        /// <summary>
        /// The sphere collider for the carryable object.
        /// </summary>
        [SerializeField] protected SphereCollider sphereCollider;

        /// <summary>
        /// The trail renderer for the carryable object.
        /// </summary>
        [SerializeField] protected TrailRenderer trailRenderer;

        /// <summary>
        /// Gets the rigidbody attached to the carryable object.
        /// </summary>
        public Rigidbody AttachedRigidbody { get; private set; }

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            AttachedRigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Called when the object becomes disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            trailRenderer.Clear();
        }

#endregion

#region Methods

        /// <summary>
        /// Despawns the carryable object.
        /// </summary>
        public abstract void Despawn();

#endregion
    }
}