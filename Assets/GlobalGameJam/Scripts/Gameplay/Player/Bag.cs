using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Bag
    {
        private readonly SpriteRenderer spriteRenderer;

        public PickableObjectData Contents;
        
        public Bag(SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
            this.spriteRenderer.sprite = null;
        }

        public void Carry(PickableObjectData objectData)
        {
            Contents = objectData;
            spriteRenderer.sprite = Contents.Sprite;
        }
    }
}