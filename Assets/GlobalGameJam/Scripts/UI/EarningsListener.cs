using TMPro;
using UnityEngine;

namespace GlobalGameJam.UI
{
    public class EarningsListener : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private EventBinding<ScoreEvents.Update> onScoreUpdateEventBinding;
            
#region Lifecycle Events

        private void Awake()
        {
            onScoreUpdateEventBinding = new EventBinding<ScoreEvents.Update>(OnScoreUpdateEventHandler);
        }

        private void OnEnable()
        {
            EventBus<ScoreEvents.Update>.Register(onScoreUpdateEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ScoreEvents.Update>.Deregister(onScoreUpdateEventBinding);
        }

        private void Reset()
        {
            text = GetComponent<TMP_Text>();
        }

#endregion

#region Event Handlers

        private void OnScoreUpdateEventHandler(ScoreEvents.Update @event)
        {
            text.text = $"Today's Earnings: <sprite index=\"0\"> {@event.Value}";
        }

#endregion
    }
}