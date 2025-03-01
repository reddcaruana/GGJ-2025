using UnityEngine;
using Obvious.Soap;

namespace WitchesBasement.System
{
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(Item), menuName = "Soap/ScriptableEvents/"+ nameof(Item))]
    internal class ScriptableEventItem : ScriptableEvent<Item>
    {
        
    }
}
