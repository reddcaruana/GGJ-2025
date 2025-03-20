using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.SceneManagement
{
    [CreateAssetMenu(fileName = "ScriptableVariable" + nameof(SceneProperties), menuName = "Soap/ScriptableVariables/"+ nameof(SceneProperties))]
    public class ScenePropertiesVariable : ScriptableVariable<SceneProperties>
    {
        
    }
}

