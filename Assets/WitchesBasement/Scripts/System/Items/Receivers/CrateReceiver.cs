using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class CrateReceiver : ItemReceiverBase<PotionData>
    {
        [SerializeField] private ScriptableListPotionData potionList;
        [SerializeField] private IntVariable sessionScore;
        [SerializeField] private IntVariable pootionCount;

        private PotionData pootionData;
        
#region Lifecycle Events

        private void Awake()
        {
            var registry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            pootionData = registry.Pootion;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Validate(other, out var item, out var data) == false)
            {
                return;
            }

            if (data == pootionData)
            {
                pootionCount.Value -= 1;
            }
            
            item.Use();
            potionList.Add(data);

            sessionScore.Value += data.Cost;
        }

#endregion
    }
}