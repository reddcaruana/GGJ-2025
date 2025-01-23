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