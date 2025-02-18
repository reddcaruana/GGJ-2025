using UnityEngine;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Light))]
    internal class IngredientChangedListenerLightColor : IngredientChangedListener
    {
        private Light attachedLight;
        
#region Lifecycle Events

        private void Awake()
        {
            attachedLight = GetComponent<Light>();
        }

#endregion
        
#region Overrides of BaseIngredientUpdater

        /// <inheritdoc />
        protected override void UpdateColor(Color color)
        {
            attachedLight.color = color;
        }

#endregion
    }
}