using System;

namespace WitchesBasement.Events
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        /// <summary>
        /// The event callback with parameters.
        /// </summary>
        private Action<T> onEvent = _ => { };
        
        /// <summary>
        /// The event callback without parameters.
        /// </summary>
        private Action onEventNoArgs = () => { };

        /// <inheritdoc />
        Action<T> IEventBinding<T>.OnEvent
        {
            get => onEvent;
            set => onEvent = value;
        }

        /// <inheritdoc />
        Action IEventBinding<T>.OnEventNoArgs
        {
            get => onEventNoArgs;
            set => onEventNoArgs = value;
        }

        public EventBinding(Action<T> onEvent)
        {
            this.onEvent = onEvent;
        }

        public EventBinding(Action onEventNoArgs)
        {
            this.onEventNoArgs = onEventNoArgs;
        }

        /// <summary>
        /// Adds a callback event.
        /// </summary>
        public void Add(Action @event)
        {
            onEventNoArgs += @event;
        }

        /// <summary>
        /// Adds a callback event.
        /// </summary>
        public void Add(Action<T> @event)
        {
            onEvent += @event;
        }

        /// <summary>
        /// Removes the callback event.
        /// </summary>
        public void Remove(Action @event)
        {
            onEventNoArgs -= @event;
        }

        /// <summary>
        /// Removes the callback event.
        /// </summary>
        public void Remove(Action<T> @event)
        {
            onEvent -= @event;
        }
    }
}