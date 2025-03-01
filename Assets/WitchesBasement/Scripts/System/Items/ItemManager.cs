using UnityEngine;
using UnityEngine.Pool;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class ItemManager : MonoBehaviour
    {
        [SerializeField] private Item itemPrefab;
        [SerializeField] private ScriptableEventItem returnToPoolEvent;

        private ObjectPool<Item> pool;

#region Lifecycle Events

        private void Awake()
        {
            pool = new ObjectPool<Item>(
                createFunc: OnCreateItem,
                actionOnGet: OnGetItem,
                actionOnRelease: OnReleaseItem,
                defaultCapacity: 20,
                maxSize: 300);
        }

        private void OnEnable()
        {
            returnToPoolEvent.OnRaised += OnReturnToPool;
        }

        private void OnDisable()
        {
            returnToPoolEvent.OnRaised -= OnReturnToPool;
        }

#endregion

#region Subscriptions

        private Item OnCreateItem()
        {
            var item = Instantiate(itemPrefab, transform);
            item.name = $"Item.{pool.CountAll:000}";
            
            return item;
        }

        private void OnGetItem(Item instance)
        {
            instance.gameObject.SetActive(true);
        }

        private void OnReleaseItem(Item instance)
        {
            instance.gameObject.SetActive(false);
            instance.transform.position = Vector3.down * 100;
        }

        private void OnReturnToPool(Item instance)
        {
            pool.Release(instance);
        }
        
#endregion

#region Methods

        public Item Generate(ItemData data, Transform parent)
        {
            var instance = pool.Get();
            
            instance.SetData(data);
            instance.transform.position = parent.position;
            instance.transform.rotation = parent.rotation;

            return instance;
        }

#endregion
    }
}