using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Potion : Carryable, IThrowable, IUsable, IPotionData, IShippable
    {
        private const float ExpandColliderRadius = 0.1f;

#region Methods

        public void Clear()
        {
            Data = null;
        }

#endregion

#region Overrides of Carryable

        /// <inheritdoc />
        public override void Despawn()
        {
            var potionManager = Singleton.GetOrCreateMonoBehaviour<PotionManager>();
            potionManager.Release(this);
        }

#endregion

#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            playerContext.Bag.Carry(Data);
            Despawn();
        }

#endregion

#region Implementation of IPotionData

        /// <inheritdoc />
        public PotionData Data { get; private set; }

        /// <inheritdoc />
        public void SetData(PotionData data)
        {
            if (data is null)
            {
                Clear();
                return;
            }

            Data = data;
            spriteRenderer.sprite = Data.Sprite;
            
            var spriteSize = spriteRenderer.bounds.size;
            var maxSize = Mathf.Max(spriteSize.x, spriteSize.y);
            sphereCollider.radius = maxSize * 0.5f + ExpandColliderRadius;
        }

#endregion

#region Implementation of IShippable

        /// <inheritdoc />
        public void Place()
        {
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

            AttachedRigidbody.linearVelocity = velocity;
        }

#endregion
    }
}