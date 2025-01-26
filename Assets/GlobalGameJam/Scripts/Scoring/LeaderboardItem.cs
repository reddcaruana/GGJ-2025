using TMPro;
using UnityEngine;

namespace GlobalGameJam
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text groupText;
        [SerializeField] private TMP_Text scoreText;

        public void SetData(string group, int score)
        {
            groupText.text = group;
            scoreText.text = $"${score}";
        }
    }
}