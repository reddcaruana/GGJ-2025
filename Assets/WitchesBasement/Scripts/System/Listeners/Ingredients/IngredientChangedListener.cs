using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal abstract class IngredientChangedListener : MonoBehaviour
    {
        [SerializeField] private FloatVariable transitionDuration;
        [SerializeField] private ScriptableEventIngredientData ingredientChangedEvent;

        private Color currentColor;
        private Color targetColor;
        private float targetTime;
        
#region Lifecycle Events

        private void OnEnable()
        {
            ingredientChangedEvent.OnRaised += OnIngredientChanged;
        }

        private void OnDisable()
        {
            ingredientChangedEvent.OnRaised -= OnIngredientChanged;
        }

        private void Update()
        {
            if (Time.time >= targetTime)
            {
                currentColor = targetColor;
                UpdateColor(currentColor);
            }

            var t = (targetTime - Time.time) / transitionDuration.Value;
            var color = Color.Lerp(currentColor, targetColor, 1 - t);

            UpdateColor(color);
        }

#endregion

#region Methods

        protected abstract void UpdateColor(Color color);

#endregion

#region Event Handlers

        private void OnIngredientChanged(IngredientData ingredientData)
        {
            targetColor = ingredientData.Color;
            targetTime = Time.time + transitionDuration.Value;
        }

#endregion
    }
}