using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalGameJam
{
    public class LeaderboardManager : MonoBehaviour
    {
        public List<SessionOutcome> Outcomes { get; private set; } = new();
        
#region Methods
        
        public void Add(SessionOutcome outcome)
        {
            DefocusOutcomes();
            
            Outcomes.Add(outcome);
            Outcomes = Outcomes.OrderBy(item => item.Score).ToList();
        }

        public void DefocusOutcomes()
        {
            for (var i = 0; i < Outcomes.Count; i++)
            {
                var outcome = Outcomes[i];
                outcome.Highlight = false;
                Outcomes[i] = outcome;
            }
        }

#endregion
    }
}