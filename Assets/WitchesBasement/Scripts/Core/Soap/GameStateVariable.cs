using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement
{
    [CreateAssetMenu(fileName = "ScriptableVariable" + nameof(GameState), menuName = "Soap/ScriptableVariables/"+ nameof(GameState))]
    public class GameStateVariable : ScriptableVariable<GameState>
    {
        
    }
}

