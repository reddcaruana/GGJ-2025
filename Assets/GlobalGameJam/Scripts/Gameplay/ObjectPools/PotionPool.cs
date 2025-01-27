using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages potions.
    /// </summary>
    public class PotionPool : CarryablePool<Potion, PotionData>
    {
        /// <summary>
        /// Releases the specified potion instance back to the pool after clearing it.
        /// </summary>
        /// <param name="instance">The potion instance to be released.</param>
        protected override void ReleaseInstance(Potion instance)
        {
            instance.Clear();
            base.ReleaseInstance(instance);
        }

#region Overrides of CarryableManager<Potion,PotionData>

        /// <summary>
        /// Generates a new potion object with the given data and attaches it to the specified anchor.
        /// </summary>
        /// <param name="data">The data used to initialize the potion object.</param>
        /// <param name="anchor">The transform to which the potion object will be attached.</param>
        /// <returns>The generated potion object.</returns>
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