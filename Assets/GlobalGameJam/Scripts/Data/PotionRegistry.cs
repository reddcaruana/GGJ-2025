using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "REG_Potions", menuName = "Custom/Registries/Potions", order = 0)]
    public class PotionRegistry : ScriptableObject
    {
        [field: SerializeField] public PotionData[] Potions { get; private set; }
    }
}