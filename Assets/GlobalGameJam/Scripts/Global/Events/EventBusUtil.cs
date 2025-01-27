using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GlobalGameJam
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<System.Type> EventTypes { get; set; }
        public static IReadOnlyList<System.Type> EventBusTypes { get; set; }

        /// <summary>
        /// Removes all listeners from all event buses.
        /// </summary>
        public static void ClearAllBuses()
        {
            foreach (var busType in EventBusTypes)
            {
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }
        }
        
        /// <summary>
        /// Initializes the class at runtime.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        /// <summary>
        /// Initializes all types of EventBuses available in the assembly.
        /// </summary>
        private static List<System.Type> InitializeAllBuses()
        {
            var eventBusTypes = new List<System.Type>();
            var typedef = typeof(EventBus<>);

            foreach (var eventType in EventTypes)
            {
                var busType = typedef.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
            }

            return eventBusTypes;
        }
        
#if UNITY_EDITOR
        
        public static UnityEditor.PlayModeStateChange PlayModeState { get; set; }

        [UnityEditor.InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /// <summary>
        /// Clears all event buses when play mode is exited.
        /// </summary>
        private static void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            PlayModeState = state;
            if (state is UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllBuses();
            }
        }
#endif
    }
}