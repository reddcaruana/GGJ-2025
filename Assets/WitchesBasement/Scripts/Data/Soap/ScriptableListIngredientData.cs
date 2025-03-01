using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.Data
{
    
    
    [CreateAssetMenu(fileName = "scriptable_list_" + nameof(IngredientData), menuName = "Soap/ScriptableLists/"+ nameof(IngredientData))]
    public class ScriptableListIngredientData : ScriptableList<IngredientData>
    {
        
    }
}
