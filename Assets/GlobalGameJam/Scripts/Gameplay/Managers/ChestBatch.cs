using System.Linq;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ChestBatch : MonoBehaviour
    {
        [SerializeField] private ChestStorage[] chests;

#region Lifecycle Events

        private void Reset()
        {
            chests = GetComponentsInChildren<ChestStorage>();
        }

#endregion
    }
}