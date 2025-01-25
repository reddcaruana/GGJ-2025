using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class IngredientManager : CarryableManager<Ingredient, IngredientData>
    {
#region Overrides of CarryableManager<Ingredient,IngredientData>

        /// <inheritdoc />
        protected override void ReleaseInstance(Ingredient instance)
        {
            instance.Clear();
            base.ReleaseInstance(instance);
        }

#endregion
        
#region Overrides of CarryableManager<Ingredient,IngredientData>

        /// <inheritdoc />
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