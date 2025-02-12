using UnityEngine;

namespace WitchesBasement.Data
{
    public abstract class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int LitterDeduction { get; private set; }
    }
}