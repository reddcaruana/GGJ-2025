using UnityEngine;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [CreateAssetMenu(fileName = "scriptable_list_" + nameof(IngredientData), menuName = "Soap/ScriptableLists/"+ nameof(IngredientData))]
    public class ScriptableListIngredientData : ScriptableList<IngredientData>
    {
        
    }
}
