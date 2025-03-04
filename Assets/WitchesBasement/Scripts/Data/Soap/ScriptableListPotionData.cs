using Obvious.Soap;
using UnityEngine;

namespace WitchesBasement.Data
{
    [CreateAssetMenu(fileName = "scriptable_list_" + nameof(PotionData), menuName = "Soap/ScriptableLists/"+ nameof(PotionData))]
    public class ScriptableListPotionData : ScriptableList<PotionData>
    {
        
    }
}