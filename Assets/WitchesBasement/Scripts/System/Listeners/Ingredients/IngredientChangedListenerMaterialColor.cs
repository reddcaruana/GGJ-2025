using UnityEngine;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(MeshRenderer))]
    internal class IngredientChangedListenerMaterialColor : IngredientChangedListener
    {
        private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
        private MeshRenderer attachedMeshRenderer;

#region Lifecycle Events

        private void Awake()
        {
            attachedMeshRenderer = GetComponent<MeshRenderer>();
        }

#endregion
        
#region Overrides of BaseIngredientUpdater

        /// <inheritdoc />
        protected override void UpdateColor(Color color)
        {
            attachedMeshRenderer.material.SetColor(EmissionColorID, color);
        }

#endregion
    }
}