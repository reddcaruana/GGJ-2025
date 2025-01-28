using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerBehavior[] playerBehaviors;
        [SerializeField] private ChestBatch[] chestBatches;

        private EventBinding<LevelEvents.Start> onLevelStartEventBinding;

#region Lifecycle Events

        private void Awake()
        {
            onLevelStartEventBinding = new EventBinding<LevelEvents.Start>(OnLevelStartEventHandler);
        }

        private void OnEnable()
        {
            EventBus<LevelEvents.Start>.Register(onLevelStartEventBinding);
        }

        private void OnDisable()
        {
            EventBus<LevelEvents.Start>.Deregister(onLevelStartEventBinding);
        }

        private void Reset()
        {
            playerBehaviors = GetComponentsInChildren<PlayerBehavior>();
        }

        private void Start()
        {
            var ingredientRegistry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            var randomIndex = Random.Range(0, chestBatches.Length);
            chestBatches[randomIndex].gameObject.SetActive(true);
            chestBatches[randomIndex].SetChests(ingredientRegistry.Ingredients);
        }

#endregion

#region Methods

        public void AddPlayer(int index)
        {
            if (index >= 0)
            {
                EventBus<DirectorEvents.Resume>.Raise(DirectorEvents.Resume.Default);
            }
        }

#endregion

#region Event Handlers

        private void OnLevelStartEventHandler(LevelEvents.Start @event)
        {
            for (var i = 0; i < playerBehaviors.Length; i++)
            {
                playerBehaviors[i].Bind(i);
            }
        }

#endregion
    }
}