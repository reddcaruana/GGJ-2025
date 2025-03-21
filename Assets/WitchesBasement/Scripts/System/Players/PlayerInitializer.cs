using UnityEngine;
using WitchesBasement.Players;

namespace WitchesBasement.System
{
    internal class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ScriptableDictionaryPlayerID playerMap;
        [SerializeField] private ScriptableDictionaryIndexedTransform playerSpawnMarkers;
        [SerializeField] private PlayerBehavior[] players;

#region Lifecycle Events

        private void Start()
        {
            Initialize();
        }

#endregion

#region Methods

        private void Initialize()
        {
            for (var i = 0; i < players.Length; i++)
            {
                var player = players[i];
                
                if (playerMap.ContainsKey(i) == false)
                {
                    player.gameObject.SetActive(false);
                    continue;
                }

                player.transform.position = playerSpawnMarkers[i].position;
                player.gameObject.SetActive(true);
                
                // TODO: Bind the player
            }
        }

#endregion
    }
}