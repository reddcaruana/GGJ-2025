using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public struct PlayerContext
    {
        public Movement Movement;
        public Interaction Interaction;
		public Bag Bag;
        public Throw Throw;

        public AudioSource AudioSource;
    }
}