using System.Collections;
using System.Linq;
using UnityEngine;

namespace WitchesBasement.SceneManagement
{
    internal class GameInitializer : AdditiveSceneLoaderBase
    {
        [SerializeField] private ScenePropertiesVariable[] sceneProperties;
        [SerializeField] private ScriptableEventSceneProperties sceneEvent;

#region Methods

        private void Start()
        {
            var properties = sceneProperties.Select(variable => variable.Value).ToArray(); 
            sceneEvent.Raise(properties);
            
            Destroy(gameObject);
        }

#endregion
    }
}