using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.SceneManagement
{
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(SceneProperties), menuName = "Soap/ScriptableEvents/"+ nameof(SceneProperties))]
    public class ScriptableEventSceneProperties : ScriptableEvent<SceneProperties[]>
    { }
}

