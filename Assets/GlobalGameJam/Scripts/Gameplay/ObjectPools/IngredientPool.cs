using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages ingredients.
    /// </summary>
    public class IngredientPool : CarryablePool<Ingredient, IngredientData>
    {
#region Overrides of CarryableManager<Ingredient,IngredientData>

        /// <summary>
        /// Releases the specified ingredient instance back to the pool after clearing it.
        /// </summary>
        /// <param name="instance">The ingredient instance to be released.</param>
        protected override void ReleaseInstance(Ingredient instance)
        {
            instance.Clear();
            base.ReleaseInstance(instance);
        }

#endregion

#region Overrides of CarryableManager<Ingredient,IngredientData>

        /// <summary>
        /// Generates a new ingredient object with the given data and attaches it to the specified anchor.
        /// </summary>
        /// <param name="data">The data used to initialize the ingredient object.</param>
        /// <param name="anchor">The transform to which the ingredient object will be attached.</param>
        /// <returns>The generated ingredient object.</returns>
        public override Ingredient Generate(IngredientData data, Transform anchor)
        {
            var instance = ObjectPool.Get();

            instance.SetData(data);
            instance.name = $"Ingredient_{ObjectPool.CountAll:000}";
            instance.transform.position = anchor.position;
            instance.transform.rotation = anchor.rotation;

            return instance;
        }

#endregion
    }
}