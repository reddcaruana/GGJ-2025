using UnityEngine;

namespace GlobalGameJam.Data
{
    public abstract class CarryableData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int LitterDeduction { get; private set; }
    }
}