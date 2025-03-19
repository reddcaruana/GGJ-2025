using System.Collections;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WitchesBasement.SceneManagement
{
    internal class MainMenuLoader : MonoBehaviour
    {
        [SerializeField] private StringVariable[] scenesToLoad;
        [SerializeField] private ScriptableEventNoParam completeEvent;

#region Lifecycle Events

        private IEnumerator Start()
        {
            foreach (var sceneToLoad in scenesToLoad)
            {
                var asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.Value, LoadSceneMode.Additive);
                if (asyncOperation == null)
                {
                    continue;
                }
                
                yield return new WaitUntil(() => asyncOperation.isDone);
            }
            
            completeEvent?.Raise();
            Destroy(gameObject);
        }

#endregion
    }
}