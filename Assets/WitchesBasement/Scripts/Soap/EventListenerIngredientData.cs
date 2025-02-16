using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;
using WitchesBasement.Data;

namespace WitchesBasement.Soap
{
    
    
    [AddComponentMenu("Soap/EventListeners/EventListener"+nameof(IngredientData))]
    public class EventListenerIngredientData : EventListenerGeneric<IngredientData>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<IngredientData>[] EventResponses => _eventResponses;
    
        [System.Serializable]
        public class EventResponse : EventResponse<IngredientData>
        {
            [SerializeField] private ScriptableEventIngredientData _scriptableEvent = null;
            public override ScriptableEvent<IngredientData> ScriptableEvent => _scriptableEvent;
    
            [SerializeField] private IngredientDataUnityEvent _response = null;
            public override UnityEvent<IngredientData> Response => _response;
        }
    
        [System.Serializable]
        public class IngredientDataUnityEvent : UnityEvent<IngredientData>
        {
            
        }
    }
}
