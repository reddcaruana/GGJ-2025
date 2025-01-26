using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class LeaderboardDisplay : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem itemPrefab;

        private readonly List<LeaderboardItem> generatedItems = new();

#region Lifecycle Events

        private void OnEnable()
        {
            ClearItems();
            itemPrefab.gameObject.SetActive(true);

            var leaderboardManager = Singleton.GetOrCreateMonoBehaviour<LeaderboardManager>();
            for (var i = 0; i < leaderboardManager.Outcomes.Count && i < 10; i++)
            {
                var item = Instantiate(itemPrefab, transform);
                var outcome = leaderboardManager.Outcomes[i];
                
                item.SetData(outcome.Name, outcome.Score);
            }
            
            itemPrefab.gameObject.SetActive(false);
        }

#endregion

#region Methods

        private void ClearItems()
        {
            while (generatedItems.Count > 0)
            {
                var item = generatedItems[0];
                Destroy(item.gameObject);
                generatedItems.RemoveAt(0);
            }
        }

#endregion
    }
}