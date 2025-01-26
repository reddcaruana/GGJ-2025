using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class PlayerAnimationUtility : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite frontHead;
        [SerializeField] private Sprite backHead;

#region Methods

        public void ChangeToFront()
        {
            spriteRenderer.sprite = frontHead;
        }

        public void ChangeToBack()
        {
            spriteRenderer.sprite = backHead;
        }

#endregion
    }
}