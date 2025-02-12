using System.Collections.Generic;

namespace WitchesBasement.Events
{
    public static class EventBus<T> where T : IEvent
    {
        /// <summary>
        /// The active event bindings.
        /// </summary>
        private static readonly HashSet<IEventBinding<T>> Bindings = new();

        /// <summary>
        /// Clears the event bindings.
        /// </summary>
        private static void Clear()
        {
            Bindings.Clear();
        }
        
        /// <summary>
        /// Registers an event binding.
        /// </summary>
        public static void Register(EventBinding<T> binding)
        {
            Bindings.Add(binding);
        }

        /// <summary>
        /// Removes the event binding.
        /// </summary>
        public static void Deregister(EventBinding<T> binding)
        {
            Bindings.Remove(binding);
        }

        /// <summary>
        /// Raises the event and triggers the registered bindings.
        /// </summary>
        public static void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(Bindings);

            foreach (var binding in snapshot)
            {
                if (Bindings.Contains(binding))
                {
                    binding.OnEvent.Invoke(@event);
                    binding.OnEventNoArgs.Invoke();
                }
            }
        }
    }
}