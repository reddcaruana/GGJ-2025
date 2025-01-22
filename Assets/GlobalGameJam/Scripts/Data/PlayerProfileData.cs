using UnityEngine;

namespace GlobalGameJam
{
    [CreateAssetMenu(fileName = "PRF_PlayerProfileName", menuName = "Custom/Data/PlayerProfile", order = 0)]
    public class PlayerProfileData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite ProfilePicture { get; private set; }
    }
}