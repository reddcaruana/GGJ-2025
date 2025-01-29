using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class LeaderboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text groupText;
        [SerializeField] private TMP_Text earningsText;
        [SerializeField] private TMP_Text potionsText;
        [SerializeField] private TMP_Text litterText;

        public void SetData(ScoreEntry entry)
        {
            if (groupText is not null)
            {
                groupText.text = entry.GroupName;
            }

            if (earningsText is not null)
            {
                var earnings = entry.Earnings - entry.Deductions;
                earningsText.text = $"$ {earnings}";
            }

            if (potionsText is not null)
            {
                potionsText.text =  $"<sprite index=0> {entry.PotionCount}";
            }

            if (litterText is not null)
            {
                litterText.text =  $"<sprite index=1> {entry.LitterCount}";
            }
        }
    }
}