using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Bag
    {
        private readonly Transform anchor;
        private readonly SpriteRenderer spriteRenderer;

        public PickableObjectData Contents;

        public bool IsFull => Contents is not null;
        
        public Bag(Transform anchor, SpriteRenderer spriteRenderer)
        {
            this.anchor = anchor;
            this.spriteRenderer = spriteRenderer;
            this.spriteRenderer.sprite = null;
        }

        public void Carry(PickableObjectData objectData)
        {
            Contents = objectData;
            spriteRenderer.sprite = Contents.Sprite;
            
            Debug.Log($"Bag is full: {IsFull}");
        }

        public void Clear()
        {
            Contents = null;
            spriteRenderer.sprite = null;
            
            Debug.Log($"Bag is full: {IsFull}");
        }

        public Transform GetAnchor()
        {
            return anchor;
        }
    }
}