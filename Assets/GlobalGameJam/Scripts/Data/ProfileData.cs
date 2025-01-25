using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PRF_PlayerProfileName", menuName = "Custom/Data/Player Profile", order = 0)]
    public class ProfileData : ScriptableObject
    {
        [field: SerializeField] public string Username { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}