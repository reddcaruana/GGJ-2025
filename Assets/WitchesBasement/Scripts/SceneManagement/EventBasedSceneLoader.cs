using UnityEngine;

namespace WitchesBasement.SceneManagement
{
    internal class EventBasedSceneLoader : AdditiveSceneLoaderBase
    {
        [SerializeField] private ScriptableEventSceneProperties scenePropertiesEvent;

#region Lifecycle Events

        private void OnEnable()
        {
            scenePropertiesEvent.OnRaised += OnLoadScenes;
        }

        private void OnDisable()
        {
            scenePropertiesEvent.OnRaised -= OnLoadScenes;
        }

#endregion

#region Subscriptions

        private void OnLoadScenes(SceneProperties[] sceneProperties)
        {
            StartCoroutine(LoadScenes(sceneProperties));
        }

#endregion
    }
}