using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace GlobalGameJam.Gameplay
{
    public abstract class CarryableManager<TCarryable, TData> : MonoBehaviour
        where TCarryable : Carryable
        where TData : CarryableData
    {
        [SerializeField] private TCarryable prefab;
        
        protected ObjectPool<TCarryable> ObjectPool { get; private set; }

#region Lifecycle Events

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

        public abstract TCarryable Generate(TData data, Transform anchor);

        public void Release(TCarryable instance)
        {
            ObjectPool.Release(instance);
        }

#endregion

#region Object Pool Methods

        protected virtual TCarryable CreateInstance()
        {
            var instance = Instantiate(prefab, transform);
            return instance;
        }

        protected virtual void GetInstance(TCarryable instance)
        {
            instance.gameObject.SetActive(true);
        }

        protected virtual void ReleaseInstance(TCarryable instance)
        {
            instance.gameObject.SetActive(false);
        }

#endregion
    }
}