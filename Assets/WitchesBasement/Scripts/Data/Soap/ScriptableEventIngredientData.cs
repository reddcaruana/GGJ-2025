using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.Data
{
    
    
    [CreateAssetMenu(fileName = "ScriptableEvent" + nameof(IngredientData), menuName = "Soap/ScriptableEvents/"+ nameof(IngredientData))]
    public class ScriptableEventIngredientData : ScriptableEvent<IngredientData>
    {
        
    }
    
}
