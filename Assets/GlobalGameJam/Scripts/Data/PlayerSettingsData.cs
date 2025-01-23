using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "PSD_Player_X", menuName = "Custom/Settings/Player", order = 0)]
    public class PlayerSettingsData : ScriptableObject
    {
        [field: SerializeField] public LayerMask RenderLayer { get; private set; }
        [field: SerializeField] public Rect ViewportRect { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }
    }
}