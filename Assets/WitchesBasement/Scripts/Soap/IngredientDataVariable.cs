using UnityEngine;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [CreateAssetMenu(fileName = "ScriptableVariable" + nameof(IngredientData), menuName = "Soap/ScriptableVariables/"+ nameof(IngredientData))]
    public class IngredientDataVariable : ScriptableVariable<IngredientData>
    {
            
    }
    
}
