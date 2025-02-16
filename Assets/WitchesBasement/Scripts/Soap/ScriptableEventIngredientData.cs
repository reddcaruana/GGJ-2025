using UnityEngine;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(IngredientData), menuName = "Soap/ScriptableEvents/"+ nameof(IngredientData))]
    public class ScriptableEventIngredientData : ScriptableEvent<IngredientData>
    {
        
    }
    
}
