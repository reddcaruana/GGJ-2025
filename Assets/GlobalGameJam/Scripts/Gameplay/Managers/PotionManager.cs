using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class PotionManager : CarryableManager<Potion, PotionData>
    {
        /// <inheritdoc />
        protected override void ReleaseInstance(Potion instance)
        {
            instance.Clear();
            base.ReleaseInstance(instance);
        }

#region Overrides of CarryableManager<Potion,PotionData>

        /// <inheritdoc />
        public override Potion Generate(PotionData data, Transform anchor)
        {
            var instance = ObjectPool.Get();
            
            instance.SetData(data);
            instance.name = $"Potion_{ObjectPool.CountActive:000}";
            instance.transform.position = anchor.position;
            instance.transform.rotation = anchor.rotation;

            return instance;
        }

#endregion
    }
}