using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Item : MonoBehaviour, IUsable, IThrowable
    {
        private const float ColliderExpansion = 0.1f;
        
        [SerializeField] private SphereCollider physicalCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TrailRenderer trailRenderer;
        
        private Rigidbody attachedRigidbody;

        /// <inheritdoc />
        public ItemData Data { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void Reset()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            physicalCollider = spriteRenderer?.GetComponent<SphereCollider>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

#endregion

#region Implementation of IUsable
        
        /// <inheritdoc />
        public void SetData(ItemData itemData)
        {
            Data = itemData;
            
            if (Data is null)
            {
                spriteRenderer.sprite = null;
                return;
            }

            spriteRenderer.sprite = Data.Sprite;
            var spriteSize = spriteRenderer.bounds.size;
            var maxSize = Mathf.Max(spriteSize.x, spriteSize.y);
            physicalCollider.radius = maxSize * 0.5f * ColliderExpansion;
        }

        /// <inheritdoc />
        public ItemData Use()
        {
            Destroy(gameObject);
            return Data;
        }

#endregion

#region Implementation of IThrowable

        /// <inheritdoc />
        public void Throw(Vector3 direction, float force, float angle)
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
    }
}