using Obvious.Soap;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    [CreateAssetMenu(fileName = nameof(ScriptableDictionaryPlayerID), menuName = "Soap/ScriptableDictionary/" + nameof(ScriptableDictionaryPlayerID))]
    public class ScriptableDictionaryPlayerID : ScriptableDictionary<int, PlayerInput>
    { }
}