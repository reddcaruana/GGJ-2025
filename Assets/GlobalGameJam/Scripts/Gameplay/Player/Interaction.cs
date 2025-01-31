using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Handles the interaction mechanics for the player.
    /// </summary>
    public class Interaction
    {
        /// <summary>
        /// The anchor point for the interaction.
        /// </summary>
        private readonly Transform anchor;

        /// <summary>
        /// The distance from the anchor point to check for interactions.
        /// </summary>
        private readonly float distance;

        /// <summary>
        /// The radius within which to check for interactions.
        /// </summary>
        private readonly float radius;

        /// <summary>
        /// The layer mask to filter interaction objects.
        /// </summary>
        private readonly int layer;

        /// <summary>
        /// The direction of the interaction.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class with the specified parameters.
        /// </summary>
        /// <param name="anchor">The anchor point for the interaction.</param>
        /// <param name="distance">The distance from the anchor point to check for interactions.</param>
        /// <param name="radius">The radius within which to check for interactions.</param>
        /// <param name="layer">The layer mask to filter interaction objects.</param>
        public Interaction(Transform anchor, float distance, float radius, LayerMask layer)
        {
            this.layer = layer.value;
            this.anchor = anchor;
            this.distance = distance;
            this.radius = radius;
        }

#region Methods
        
        /// <summary>
        /// Performs an interaction based on the player context.
        /// </summary>
        /// <param name="playerContext">The context of the player performing the interaction.</param>
        public void Interact(PlayerContext playerContext)
        {
            var anchorPosition = anchor.position + Direction * distance;

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

#endregion
    }
}