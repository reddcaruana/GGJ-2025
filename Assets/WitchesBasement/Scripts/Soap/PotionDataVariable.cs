using UnityEngine;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [CreateAssetMenu(fileName = "ScriptableVariable" + nameof(PotionData), menuName = "Soap/ScriptableVariables/"+ nameof(PotionData))]
    public class PotionDataVariable : ScriptableVariable<PotionData>
    {
            
    }
    
}
