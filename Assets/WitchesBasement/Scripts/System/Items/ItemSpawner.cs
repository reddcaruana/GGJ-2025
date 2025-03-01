using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    public class ItemSpawner : MonoBehaviour, IUsable
    {
        [SerializeField] private ItemData defaultData;

#region Lifecycle Event

        private void Awake()
        {
            SetData(defaultData);
        }

#endregion
        
#region Implementation of IItemData

        /// <inheritdoc />
        public ItemData Data { get; private set; }

        /// <inheritdoc />
        public void SetData(ItemData itemData)
        {
            Data = itemData;
        }

        /// <inheritdoc />
        public ItemData Use()
        {
            return Data;
        }

#endregion
    }
}