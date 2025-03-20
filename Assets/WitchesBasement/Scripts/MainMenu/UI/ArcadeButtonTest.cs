using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using WitchesBasement.SceneManagement;

namespace WitchesBasement.MainMenu
{
    public class ArcadeButtonTest : MonoBehaviour, ISubmitHandler
    {
        [SerializeField] private ScenePropertiesVariable[] sceneProperties;
        [SerializeField] private ScriptableEventSceneProperties sceneEvent;

#region Implementation of ISubmitHandler

        /// <inheritdoc />
        public void OnSubmit(BaseEventData eventData)
        {
            var properties = sceneProperties.Select(variable => variable.Value).ToArray();
            sceneEvent.Raise(properties);
        }

#endregion
    }
}