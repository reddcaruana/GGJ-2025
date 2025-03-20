using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WitchesBasement.SceneManagement
{
    internal abstract class AdditiveSceneLoaderBase : MonoBehaviour
    {
        [SerializeField] protected ScriptableEventNoParam completeEvent;

        private readonly List<SceneProperties> activeScenes = new();
        
#region Coroutines

        private IEnumerator UnloadActiveScenes()
        {
            for (var i = activeScenes.Count - 1; i >= 0; i--)
            {
                var properties = activeScenes[i];
                if (properties.Persistent)
                {
                    continue;
                }
                
                var asyncOperation = SceneManager.UnloadSceneAsync(properties.Name);
                if (asyncOperation == null)
                {
                    continue;
                }

                yield return new WaitUntil(() => asyncOperation.isDone);
                
                activeScenes.RemoveAt(i);
            }
        }

        protected IEnumerator LoadScenes(SceneProperties[] sceneProperties)
        {
            yield return UnloadActiveScenes();
            
            foreach (var properties in sceneProperties)
            {
                var scene = SceneManager.GetSceneByName(properties.Name);
                if (scene.isLoaded)
                {
                    activeScenes.Add(properties);
                    continue;
                }
                
                var asyncOperation = SceneManager.LoadSceneAsync(properties.Name, LoadSceneMode.Additive);
                if (asyncOperation == null)
                {
                    continue;
                }
                
                yield return new WaitUntil(() => asyncOperation.isDone);
                activeScenes.Add(properties);
            }
            
            completeEvent?.Raise();
        }

#endregion
    }
}