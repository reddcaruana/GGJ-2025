using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;
using WitchesBasement.Soap;

namespace WitchesBasement.System
{
    internal abstract class BaseIngredientUpdater : MonoBehaviour
    {
        [SerializeField] private FloatVariable transitionDuration;
        [SerializeField] private ScriptableEventIngredientData scriptableEvent;

        protected Color CurrentColor { get; private set; }
        protected Color TargetColor { get; private set; }
        protected float TargetTime { get; private set; }
        
#region Lifecycle Events

        private void OnEnable()
        {
            scriptableEvent.OnRaised += OnIngredientUpdated;
        }

        private void OnDisable()
        {
            scriptableEvent.OnRaised -= OnIngredientUpdated;
        }

        private void Update()
        {
            if (Time.time >= TargetTime)
            {
                CurrentColor = TargetColor;
                UpdateColor(CurrentColor);
            }

            var t = (TargetTime - Time.time) / transitionDuration.Value;
            var color = Color.Lerp(CurrentColor, TargetColor, 1 - t);

            UpdateColor(color);
        }

#endregion

#region Methods

        protected abstract void UpdateColor(Color color);

#endregion

#region Event Handlers

        private void OnIngredientUpdated(IngredientData ingredientData)
        {
            TargetColor = ingredientData.Color;
            TargetTime = Time.time + transitionDuration.Value;
            Debug.Log($"{TargetTime}");
        }

#endregion
    }
}