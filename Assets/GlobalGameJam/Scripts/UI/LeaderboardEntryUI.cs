using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class LeaderboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text groupText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timeText;

        public void SetData(ScoreEntry entry)
        {
            if (groupText is not null)
            {
                groupText.text = entry.Name;
            }

            if (scoreText is not null)
            {
                scoreText.text = $"${entry.Score}";
            }

            if (timeText is not null)
            {
                timeText.text =  $"<sprite index=1> {TimeUtility.ToString(entry.Time)}";
            }
        }
    }
}