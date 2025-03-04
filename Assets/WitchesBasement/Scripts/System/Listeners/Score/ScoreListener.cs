using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace WitchesBasement.System
{
    internal class ScoreListener : MonoBehaviour
    {
        [SerializeField] private IntVariable scoreValue;
        [SerializeField] private TMP_Text scoreText;

#region Lifecycle Events

        private void OnEnable()
        {
            scoreValue.OnValueChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            scoreValue.OnValueChanged += OnScoreChanged;
        }
        
#endregion

#region Subscriptions

        private void OnScoreChanged(int value)
        {
            scoreText.text = $"${value:N0}";
        }

#endregion
    }
}