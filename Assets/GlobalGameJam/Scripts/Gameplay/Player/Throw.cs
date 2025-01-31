using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Handles the throwing mechanics for the player.
    /// </summary>
    public class Throw
    {
        /// <summary>
        /// The speed at which the object is thrown.
        /// </summary>
        private readonly float speed;

        /// <summary>
        /// The angle at which the object is thrown.
        /// </summary>
        private readonly float angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Throw"/> class with the specified speed and angle.
        /// </summary>
        /// <param name="speed">The speed of the throw.</param>
        /// <param name="angle">The angle of the throw.</param>
        public Throw(float speed, float angle)
        {
            this.speed = speed;
            this.angle = angle;
        }

#region Methods

        /// <summary>
        /// Drops the ingredient from the bag in the specified direction.
        /// </summary>
        /// <param name="bag">The bag containing the ingredient.</param>
        /// <param name="direction">The direction to drop the ingredient.</param>
        public void Drop(Bag bag, Vector3 direction)
        {
            if (bag.Contents is not IngredientData ingredientData)
            {
                return;
            }

            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientPool>();
            var ingredient = ingredientManager.Generate(ingredientData, bag.GetAnchor());
            ingredient.Throw(direction, speed, angle);

            bag.Clear();
        }

#endregion
    }
}