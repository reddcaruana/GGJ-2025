using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the light component to change its color based on game events.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class CauldronLight : CauldronColorChangeListener
    {
        private Light attachedLight;
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
            var startingColor = attachedLight.color;

            while (Time.time < targetTime)
            {
                var t = (targetTime - Time.time) / duration;
                attachedLight.color = Color.Lerp(startingColor, targetColor, 1 - t);
                yield return null;
            }

            attachedLight.color = targetColor;
            activeCoroutine = null;
        }

#endregion

#region Overrides of CauldronColorChangeListener

        /// <summary>
        /// Initializes the attached light component and event binding.
        /// </summary>
        protected override void Awake()
        {
            attachedLight = GetComponent<Light>();
            base.Awake();
        }
        
        /// <summary>
        /// Event handler for when the expected ingredient changes.
        /// Stops any active color change coroutine and starts a new one.
        /// </summary>
        /// <param name="event">The event data containing the new ingredient and color change duration.</param>
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