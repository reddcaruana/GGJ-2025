using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Movement
    {
        private readonly float speed;
        private readonly Rigidbody rigidbody;

        public Movement(float speed, Rigidbody rigidbody)
        {
            this.speed = speed;
            this.rigidbody = rigidbody;
        }

        public void Move(Vector3 inputDirection)
        {
            rigidbody.linearVelocity = inputDirection * speed;
        }
    }
}