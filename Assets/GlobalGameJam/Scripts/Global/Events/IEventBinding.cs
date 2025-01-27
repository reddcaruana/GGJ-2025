using System;

namespace GlobalGameJam
{
    public interface IEventBinding<T>
    {
        /// <summary>
        /// The event callback with parameters.
        /// </summary>
        public Action<T> OnEvent { get; set; }
        
        /// <summary>
        /// The event callback without parameters.
        /// </summary>
        public Action OnEventNoArgs { get; set; }
    }
}