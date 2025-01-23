using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "REG_PlayerSettingsRegistry", menuName = "Custom/Registries/PlayerSettings", order = 0)]
    public class PlayerSettingsRegistry : ScriptableObject
    {
        [field: SerializeField] public PlayerSettingsData[] Settings { get; private set; }
    }
}