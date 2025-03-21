using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class PlayerSpawnMarker : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private ScriptableDictionaryIndexedTransform playerSpawnMarkerList;

#region Lifecycle Events

        private void OnEnable()
        {
            if (playerSpawnMarkerList.ContainsKey(index))
            {
                Debug.LogWarning($"Spawn marker {index} already registered. Consider updating this in Edit mode.");
                index = Mathf.Max(playerSpawnMarkerList.Keys.ToArray()) + 1;
            }
            
            playerSpawnMarkerList.Add(index, transform);
        }

        private void OnDisable()
        {
            playerSpawnMarkerList.Remove(index);
        }

#endregion
    }
}