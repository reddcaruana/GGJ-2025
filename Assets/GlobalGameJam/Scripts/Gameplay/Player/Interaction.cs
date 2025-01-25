using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Interaction
    {
        private readonly Transform anchor;
        private readonly float distance;
        private readonly float radius;

        private readonly int layer;
        
        public Direction Direction;

        public Interaction(Transform anchor, float distance, float radius, LayerMask layer)
        {
            this.layer = layer.value;
            this.anchor = anchor;
            this.distance = distance;
            this.radius = radius;
        }

        public void Interact(PlayerContext playerContext)
        {
            var anchorPosition = anchor.position + Direction.ToVector() * distance;

            var results = new Collider[8];
            var numCollisions = Physics.OverlapSphereNonAlloc(anchorPosition, radius, results, layer);
            
            float closestDistance = float.MaxValue;
            Collider closestCollider = null;
                
            for (var i = 0; i < numCollisions; i++)
            {
                var result = results[i];
                var closest = Vector3.Distance(result.transform.position, anchorPosition);

                if (closest < closestDistance)
                {
                    closestDistance = closest;
                    closestCollider = result;
                }
            }

            var usable = closestCollider?.GetComponentInParent<IUsable>();
            usable?.Use(playerContext);
        }
    }
}