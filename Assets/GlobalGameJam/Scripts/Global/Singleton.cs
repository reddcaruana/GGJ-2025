using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public static class Singleton
    {
        /// <summary>
        /// The registered singleton instances.
        /// </summary>
        private static readonly Dictionary<System.Type, object> RegisteredSingletons = new();

        /// <summary>
        /// Clear the registered systems when the game launches.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            RegisteredSingletons.Clear();
        }

        /// <summary>
        /// Returns a singleton instance of a MonoBehaviour or creates one if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the MonoBehaviour singleton to get or create.</typeparam>
        /// <returns>The singleton instance of the specified MonoBehaviour type.</returns>
        public static T GetOrCreateMonoBehaviour<T>() where T : MonoBehaviour
        {
            if (RegisteredSingletons.TryGetValue(typeof(T), out var instance) && instance is not null)
            {
                return (T)instance;
            }

            instance = Object.FindAnyObjectByType<T>();
            if (instance != null)
            {
                return (T)instance;
            }

            var gameObject = new GameObject(typeof(T).Name);
            instance = gameObject.AddComponent<T>();
            Register(instance);

            return (T)instance;
        }

        /// <summary>
        /// Returns a singleton instance of a ScriptableObject or creates one if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the ScriptableObject singleton to get or create.</typeparam>
        /// <returns>The singleton instance of the specified ScriptableObject type.</returns>
        public static T GetOrCreateScriptableObject<T>() where T : ScriptableObject
        {
            if (RegisteredSingletons.TryGetValue(typeof(T), out var instance) && instance is not null)
            {
                return (T)instance;
            }

            var instances = Resources.LoadAll<T>("Registries");
            if (instances.Length == 1)
            {
                return instances[0];
            }

            if (instances.Length > 1)
            {
                Debug.LogWarning($"Multiple assets of type {typeof(T).Name} detected. This may cause issues.");
                return instances[0];
            }

            Debug.LogWarning($"No asset of type {typeof(T).Name} detected. Creating a temporary instance.");

            instance = ScriptableObject.CreateInstance<T>();
            Register(instance);

            return (T)instance;
        }

        /// <summary>
        /// Registers a singleton instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the singleton to register.</typeparam>
        /// <param name="singleton">The singleton instance to register.</param>
        public static void Register<T>(T singleton)
        {
            if (RegisteredSingletons.TryGetValue(typeof(T), out var instance) && instance is not null)
            {
                Debug.LogWarning($"A Singleton of type {typeof(T).Name} is already registered.");
                return;
            }

            RegisteredSingletons[typeof(T)] = singleton;
        }

        /// <summary>
        /// Removes a singleton instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the singleton to remove.</typeparam>
        public static void Remove<T>()
        {
            if (RegisteredSingletons.ContainsKey(typeof(T)) == false)
            {
                return;
            }

            RegisteredSingletons.Remove(typeof(T));
        }

        /// <summary>
        /// Tries to get a singleton instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the singleton to get.</typeparam>
        /// <param name="output">The output parameter that will contain the singleton instance if found.</param>
        /// <returns>True if the singleton instance is found; otherwise, false.</returns>
        public static bool TryGet<T>(out T output)
        {
            output = (T)RegisteredSingletons.GetValueOrDefault(typeof(T));
            return RegisteredSingletons.ContainsKey(typeof(T));
        }
    }
}