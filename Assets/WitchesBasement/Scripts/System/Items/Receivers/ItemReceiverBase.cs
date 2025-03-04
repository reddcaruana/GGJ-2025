using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal abstract class ItemReceiverBase<T> : MonoBehaviour
        where T : ItemData
    {
#region Methods

        protected bool Validate(Collider other, out Item itemOutput, out T dataOutput)
        {
            itemOutput = null;
            dataOutput = null;
            
            if (other.CompareTag("Item") == false)
            {
                return false;
            }

            if (other.TryGetComponent(out Item item) == false)
            {
                return false;
            }

            if (item.Data is not T data)
            {
                return false;
            }

            itemOutput = item;
            dataOutput = data;
            return true;
        }

#endregion
    }
}