using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "REG_PlayerProfiles", menuName = "Custom/Registries/Profiles", order = 0)]
    public class ProfileRegistry : ScriptableObject
    {
        [field: SerializeField] public PlayerProfileData[] Profiles { get; private set; }
    }
}