using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Handles the movement mechanics for the player.
    /// </summary>
    public class Movement
    {
        /// <summary>
        /// The speed at which the player moves.
        /// </summary>
        private readonly float speed;

        /// <summary>
        /// The rigidbody component attached to the player.
        /// </summary>
        private readonly Rigidbody rigidbody;

        /// <summary>
        /// Initializes a new instance of the <see cref="Movement"/> class with the specified speed and rigidbody.
        /// </summary>
        /// <param name="speed">The speed of the player.</param>
        /// <param name="rigidbody">The rigidbody component attached to the player.</param>
        public Movement(float speed, Rigidbody rigidbody)
        {
            this.speed = speed;
            this.rigidbody = rigidbody;
        }

#region Methods
        
        /// <summary>
        /// Moves the player in the specified direction.
        /// </summary>
        /// <param name="inputDirection">The direction to move the player.</param>
        public void Move(Vector3 inputDirection)
        {
            rigidbody.linearVelocity = inputDirection * speed;
        }

#endregion
    }
}