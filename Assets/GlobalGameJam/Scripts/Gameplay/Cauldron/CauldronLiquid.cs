using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Listens for color change events to update the cauldron liquid color.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class CauldronLiquid : CauldronColorChangeListener
    {
        private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
        private MeshRenderer meshRenderer;
        private Coroutine activeCoroutine;

#region Coroutines

        /// <summary>
        /// Coroutine to change the light color over a specified duration.
        /// </summary>
        /// <param name="targetColor">The target color to change to.</param>
        /// <param name="duration">The duration over which to change the color.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator ChangeColorRoutine(Color targetColor, float duration)
        {
            var targetTime = Time.time + duration;
            var startingColor = meshRenderer.material.GetColor(EmissionColorID);

            while (Time.time < targetTime)
            {
                var t = (targetTime - Time.time) / duration;
                var color = Color.Lerp(startingColor, targetColor, 1 - t);
                meshRenderer.material.SetColor(EmissionColorID, color);

                yield return null;
            }
            meshRenderer.material.SetColor(EmissionColorID, targetColor);

            activeCoroutine = null;
        }

#endregion

#region Overrides of CauldronColorChangeListener

        /// <inheritdoc />
        protected override void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            base.Awake();
        }

        /// <inheritdoc />
        protected override void OnChangedExpectedIngredientHandler(CauldronEvents.ChangedExpectedIngredient @event)
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }

            activeCoroutine = StartCoroutine(ChangeColorRoutine(@event.Ingredient.Color, @event.ColorChangeDuration));
        }

#endregion
    }
}