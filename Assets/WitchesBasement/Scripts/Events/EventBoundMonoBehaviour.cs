using System.Collections.Generic;
using UnityEngine;

namespace WitchesBasement.Events
{
    public abstract class EventBoundMonoBehaviour : MonoBehaviour
    {
        private readonly Dictionary<System.Type, object> eventBindings = new();

#region Lifecycle Events

        protected virtual void OnEnable()
        {
            InitializeEventBindings();
        }

        protected virtual void OnDisable()
        {
            foreach (var binding in eventBindings.Values)
            {
                var eventType = binding.GetType().GetGenericArguments()[0];
                var deregisterMethod = typeof(EventBus<>).MakeGenericType(eventType).GetMethod("Deregister");
                deregisterMethod?.Invoke(null, new[] { binding });
            }
            
            eventBindings.Clear();
        }

#endregion
        
#region Methods

        protected void RegisterEvent<TEvent>(System.Action<TEvent> eventHandler) where TEvent : IEvent
        {
            var eventBinding = new EventBinding<TEvent>(eventHandler);
            eventBindings[typeof(TEvent)] = eventBinding;
            EventBus<TEvent>.Register(eventBinding);
        }

        protected void DeregisterEvent<TEvent>() where TEvent : IEvent
        {
            if (eventBindings.TryGetValue(typeof(TEvent), out var binding))
            {
                EventBus<TEvent>.Deregister((EventBinding<TEvent>)binding);
                eventBindings.Remove(typeof(TEvent));
            }
        }

        protected abstract void InitializeEventBindings();

#endregion
    }
}