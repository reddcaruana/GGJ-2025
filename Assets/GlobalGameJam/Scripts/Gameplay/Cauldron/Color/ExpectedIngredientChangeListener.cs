using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Abstract base class for listening to cauldron color change events.
    /// </summary>
    public abstract class ExpectedIngredientChangeListener : MonoBehaviour
    {
        private EventBinding<CauldronEvents.ChangedExpectedIngredient> onChangedExpectedIngredientBinding;

#region Lifecycle Events

        /// <summary>
        /// Initializes the event binding for the expected ingredient change event.
        /// </summary>
        protected virtual void Awake()
        {
            onChangedExpectedIngredientBinding = new EventBinding<CauldronEvents.ChangedExpectedIngredient>(OnChangedExpectedIngredientHandler);
        }

        /// <summary>
        /// Registers the event binding when the object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            EventBus<CauldronEvents.ChangedExpectedIngredient>.Register(onChangedExpectedIngredientBinding);
        }

        /// <summary>
        /// Deregisters the event binding when the object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            EventBus<CauldronEvents.ChangedExpectedIngredient>.Deregister(onChangedExpectedIngredientBinding);
        }

#endregion

#region Abstract Methods

        /// <summary>
        /// Event handler for when the expected ingredient changes.
        /// Stops any active color change coroutine and starts a new one.
        /// </summary>
        /// <param name="event">The event data containing the new ingredient and color change duration.</param>
        protected abstract void OnChangedExpectedIngredientHandler(CauldronEvents.ChangedExpectedIngredient @event);

#endregion
    }
}