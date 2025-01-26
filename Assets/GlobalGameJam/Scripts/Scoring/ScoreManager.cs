using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;

namespace GlobalGameJam
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        private int score;
        private int time;
        private string group;

        private ShippingBin shippingBin;

#region Methods
        
        public void Add(PotionData potionData)
        {
            score += score;
            time += time;
        }

        public void Clear()
        {
            score = 0;
            time = 0;
            group = string.Empty;
        }

        public SessionOutcome GetOutcome()
        {
            return new SessionOutcome
            {
                Name = group,
                Score = score,
                Time = time
            };
        }

        public void SetGroup(string newGroup)
        {
            group = newGroup;
        }

#endregion

#region Binding

        public void Bind(ShippingBin targetShippingBin)
        {
            shippingBin = targetShippingBin;
            shippingBin.OnUse += OnShippingBinUsedHandler;
        }

        public void Release()
        {
            if (shippingBin is not null)
            {
                shippingBin.OnUse -= OnShippingBinUsedHandler;
                shippingBin = null;
            }
        }

#endregion

#region Event Handlers

        private void OnShippingBinUsedHandler(PotionData data)
        {
            score += data.Cost;
            scoreText.text = score.ToString();
        }

#endregion
    }
}