using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement
{
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(GameState), menuName = "Soap/ScriptableEvents/"+ nameof(GameState))]
    public class ScriptableEventGameState : ScriptableEvent<GameState>
    {
    
    }
}

