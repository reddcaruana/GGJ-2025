using UnityEngine;

namespace WitchesBasement.Data
{
    [CreateAssetMenu(fileName = "REG_PlayerProfiles", menuName = "Custom/Registries/Profiles", order = 0)]
    public class ProfileRegistry : ScriptableObject
    {
        [field: SerializeField] public ProfileData[] Profiles { get; private set; }

#region Context Menu
        
        [ContextMenu("Find Profiles")]
        private void FindProfiles()
        {
            Profiles = Resources.LoadAll<ProfileData>("Profiles");
        }

#endregion
    }
}