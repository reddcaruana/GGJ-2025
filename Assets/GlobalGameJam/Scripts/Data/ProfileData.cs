using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PRF_PlayerProfileName", menuName = "Custom/Data/Player Profile", order = 0)]
    public class ProfileData : ScriptableObject
    {
        [field: SerializeField] public string Username { get; private set; }
        [field: SerializeField] public int PasswordLength { get; private set; }
        [field: SerializeField, Range(1, 3)] public int Attempts { get; private set; } = 1;
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}