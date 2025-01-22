using UnityEngine;

namespace GlobalGameJam.Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private Vector3 direction;

        private Rigidbody attachedRigidbody;

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            attachedRigidbody.linearVelocity = direction * speed;
        }

        private void Reset()
        {
            Awake();
            attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

#endregion

#region Methods

        public void SetDirection(Vector3 newDirection)
        {
            direction = newDirection;
        }

#endregion
    }
}