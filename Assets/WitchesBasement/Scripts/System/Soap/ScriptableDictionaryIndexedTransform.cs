using System;
using UnityEngine;
using Obvious.Soap;

namespace WitchesBasement.System
{
    
    
    [CreateAssetMenu(fileName = nameof(ScriptableDictionaryIndexedTransform), menuName = "Soap/ScriptableDictionary/"+nameof(ScriptableDictionaryIndexedTransform))]
    public class ScriptableDictionaryIndexedTransform : ScriptableDictionary<int,Transform>
    {
        
    }
}
