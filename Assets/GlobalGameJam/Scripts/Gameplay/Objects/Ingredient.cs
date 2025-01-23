using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ingredient : MonoBehaviour, IUsable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SphereCollider sphereCollider;
        
        private Rigidbody attachedRigidbody;
        private PickableObjectData pickableObjectData;

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

#endregion

#region Methods

        public void SetData(PickableObjectData objectData)
        {
            pickableObjectData = objectData;
            spriteRenderer.sprite = pickableObjectData.Sprite;

            var spriteSize = spriteRenderer.bounds.size;
            var maxSize = Mathf.Max(spriteSize.x, spriteSize.y);
            sphereCollider.radius = maxSize * 0.5f;
        }

        public void Launch(Vector3 direction, float force, float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;
            
            var velocity = new Vector3(
                direction.x * force * Mathf.Cos(angleRad),
                force * Mathf.Sin(angleRad),
                direction.z * force * Mathf.Cos(angleRad)
            );

            attachedRigidbody.linearVelocity = velocity;
        }

#endregion

#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            playerContext.Bag.Carry(pickableObjectData);
        }

#endregion

    }
}