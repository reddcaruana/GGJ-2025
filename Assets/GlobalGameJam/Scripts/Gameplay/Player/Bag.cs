using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a bag that can carry objects in the game.
    /// </summary>
    public class Bag
    {
        /// <summary>
        /// The anchor point for the bag.
        /// </summary>
        private readonly Transform anchor;

        /// <summary>
        /// The sprite renderer for displaying the carried object.
        /// </summary>
        private readonly SpriteRenderer spriteRenderer;

        /// <summary>
        /// The data of the object currently carried in the bag.
        /// </summary>
        public CarryableData Contents;

        /// <summary>
        /// Gets a value indicating whether the bag is full.
        /// </summary>
        public bool IsFull => Contents is not null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bag"/> class with the specified anchor and sprite renderer.
        /// </summary>
        /// <param name="anchor">The anchor point for the bag.</param>
        /// <param name="spriteRenderer">The sprite renderer for displaying the carried object.</param>
        public Bag(Transform anchor, SpriteRenderer spriteRenderer)
        {
            this.anchor = anchor;
            this.spriteRenderer = spriteRenderer;
            this.spriteRenderer.sprite = null;
        }

#region Methods
        
        /// <summary>
        /// Carries the specified object data in the bag.
        /// </summary>
        /// <param name="objectData">The data of the object to carry.</param>
        public void Carry(CarryableData objectData)
        {
            Contents = objectData;
            spriteRenderer.sprite = Contents.Sprite;
        }

        /// <summary>
        /// Clears the contents of the bag.
        /// </summary>
        public void Clear()
        {
            Contents = null;
            spriteRenderer.sprite = null;
        }

        /// <summary>
        /// Gets the anchor point of the bag.
        /// </summary>
        /// <returns>The anchor point of the bag.</returns>
        public Transform GetAnchor()
        {
            return anchor;
        }

#endregion
    }
}