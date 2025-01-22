using GlobalGameJam.Players;
using UnityEngine;

namespace GlobalGameJam.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerBehavior[] playerBehaviors;

#region Lifecycle Events

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            playerBehaviors[0].Bind(0);
        }

#endregion
    }
}