using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [AddComponentMenu("Soap/EventListeners/EventListener"+nameof(PotionData))]
    public class EventListenerPotionData : EventListenerGeneric<PotionData>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<PotionData>[] EventResponses => _eventResponses;
    
        [System.Serializable]
        public class EventResponse : EventResponse<PotionData>
        {
            [SerializeField] private ScriptableEventPotionData _scriptableEvent = null;
            public override ScriptableEvent<PotionData> ScriptableEvent => _scriptableEvent;
    
            [SerializeField] private PotionDataUnityEvent _response = null;
            public override UnityEvent<PotionData> Response => _response;
        }
    
        [System.Serializable]
        public class PotionDataUnityEvent : UnityEvent<PotionData>
        {
            
        }
    }
}
