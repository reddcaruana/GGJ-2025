using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.Data
{
    
    
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(PotionData), menuName = "Soap/ScriptableEvents/"+ nameof(PotionData))]
    public class ScriptableEventPotionData : ScriptableEvent<PotionData>
    {
        
    }
    
}
