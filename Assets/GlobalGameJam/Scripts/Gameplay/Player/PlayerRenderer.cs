using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerRenderer : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer[] SpriteRenderers { get; private set; }
    }
}