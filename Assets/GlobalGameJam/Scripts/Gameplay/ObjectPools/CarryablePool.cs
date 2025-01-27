using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages carryable objects.
    /// </summary>
    /// <typeparam name="TCarryable">The type of carryable object.</typeparam>
    /// <typeparam name="TData">The type of data associated with the carryable object.</typeparam>
    public abstract class CarryablePool<TCarryable, TData> : MonoBehaviour
        where TCarryable : Carryable
        where TData : CarryableData
    {
        /// <summary>
        /// The prefab used to create new instances of the carryable object.
        /// </summary>
        [SerializeField] private TCarryable prefab;

        /// <summary>
        /// The object pool for managing carryable instances.
        /// </summary>
        protected ObjectPool<TCarryable> ObjectPool { get; private set; }

#region Lifecycle Events

        /// <summary>
        /// Initializes the object pool with specified creation, activation, and deactivation actions.
        /// </summary>
        protected virtual void Awake()
        {
            ObjectPool = new ObjectPool<TCarryable>(
                createFunc: CreateInstance,
                actionOnGet: GetInstance,
                actionOnRelease: ReleaseInstance,
                defaultCapacity: 100,
                maxSize: 200);
        }

#endregion

#region Methods

        /// <summary>
        /// Generates a new carryable object with the given data and attaches it to the specified anchor.
        /// </summary>
        /// <param name="data">The data used to initialize the carryable object.</param>
        /// <param name="anchor">The transform to which the carryable object will be attached.</param>
        /// <returns>The generated carryable object.</returns>
        public abstract TCarryable Generate(TData data, Transform anchor);

        /// <summary>
        /// Releases the specified carryable object back to the pool.
        /// </summary>
        /// <param name="instance">The carryable object to be released.</param>
        public void Release(TCarryable instance)
        {
            ObjectPool.Release(instance);
        }

#endregion

#region Object Pool Methods

        /// <summary>
        /// Creates a new instance of the carryable object.
        /// </summary>
        /// <returns>The created carryable object instance.</returns>
        protected virtual TCarryable CreateInstance()
        {
            var instance = Instantiate(prefab, transform);
            return instance;
        }

        /// <summary>
        /// Activates the specified carryable object instance.
        /// </summary>
        /// <param name="instance">The carryable object instance to be activated.</param>
        protected virtual void GetInstance(TCarryable instance)
        {
            instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates the specified carryable object instance.
        /// </summary>
        /// <param name="instance">The carryable object instance to be deactivated.</param>
        protected virtual void ReleaseInstance(TCarryable instance)
        {
            instance.gameObject.SetActive(false);
        }

#endregion
    }
}