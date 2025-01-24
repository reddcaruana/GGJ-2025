using System;
using UnityEngine;

namespace GlobalGameJam.Gameplay.Cauldron
{
    public class Lever : MonoBehaviour, IUsable
    {
        private CauldronManager cauldronManager;

#region Lifecycle Events

        private void Awake()
        {
            cauldronManager = Singleton.GetOrCreateMonoBehaviour<CauldronManager>();
        }

#endregion

#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            cauldronManager.Brew();
        }

#endregion
    }
}