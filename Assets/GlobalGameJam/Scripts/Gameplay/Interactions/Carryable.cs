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

        /// <summary>
        /// Event binding for the LevelEvents.End event.
        /// </summary>
        private EventBinding<LevelEvents.End> onLevelEndEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            AttachedRigidbody = GetComponent<Rigidbody>();

            onLevelEndEventBinding = new EventBinding<LevelEvents.End>(OnLevelEndEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the LevelEvents.End event binding.
        /// </summary>
        protected virtual void OnEnable()
        {
            EventBus<LevelEvents.End>.Register(onLevelEndEventBinding);
        }

        /// <summary>
        /// Called when the object becomes disabled.
        /// Clears the trail renderer and deregisters the LevelEvents.End event binding.
        /// </summary>
        protected virtual void OnDisable()
        {
            trailRenderer.Clear();
            
            EventBus<LevelEvents.End>.Deregister(onLevelEndEventBinding);
        }

#endregion

#region Methods

        /// <summary>
        /// Despawns the carryable object.
        /// </summary>
        public abstract void Despawn();

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the LevelEvents.End event.
        /// </summary>
        /// <param name="event">The LevelEvents.End event.</param>
        protected abstract void OnLevelEndEventHandler(LevelEvents.End @event);

#endregion
    }
}