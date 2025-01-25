using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace GlobalGameJam.Gameplay
{
    public class IngredientManager : MonoBehaviour
    {
        [SerializeField] private Ingredient ingredientPrefab;
        
        private ObjectPool<Ingredient> objectPool;

#region Lifecycle Events

        private void Awake()
        {
            objectPool = new ObjectPool<Ingredient>(
                createFunc: CreateIngredient,
                actionOnGet: GetIngredient,
                actionOnRelease: ReleaseIngredient,
                defaultCapacity: 100,
                maxSize: 200);
        }

#endregion

#region Methods

        public Ingredient Generate(IngredientData pickableObjectData, Transform anchor)
        {
            var instance = objectPool.Get();
            
            instance.SetData(pickableObjectData);
            instance.name = $"Ingredient_{objectPool.CountAll:000}";
            instance.transform.position = anchor.position;
            instance.transform.rotation = anchor.rotation;

            return instance;
        }

        public void Release(Ingredient instance)
        {
            objectPool.Release(instance);
        }

#endregion

#region Object Pool Methods

        private Ingredient CreateIngredient()
        {
            var instance = Instantiate(ingredientPrefab, transform);
            return instance;
        }

        private void GetIngredient(Ingredient instance)
        {
            instance.gameObject.SetActive(true);
        }

        private void ReleaseIngredient(Ingredient instance)
        {
            instance.Clear();
            instance.gameObject.SetActive(false);
        }

#endregion
    }
}