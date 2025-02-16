using UnityEngine;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(PotionData), menuName = "Soap/ScriptableEvents/"+ nameof(PotionData))]
    public class ScriptableEventPotionData : ScriptableEvent<PotionData>
    {
        
    }
    
}
