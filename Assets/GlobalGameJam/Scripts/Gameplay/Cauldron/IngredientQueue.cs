using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class IngredientQueue : IEnumerable<IngredientData>
    {
        public IngredientData CurrentIngredient { get; private set; }
        private Queue<IngredientData> ingredientQueue;

        public IngredientQueue(IngredientData[] ingredients)
        {
            ingredientQueue = new Queue<IngredientData>(ingredients);
        }
        
#region Implementation of IEnumerable

        /// <inheritdoc />
        public IEnumerator<IngredientData> GetEnumerator()
        {
            if (CurrentIngredient is not null)
            {
                ingredientQueue.Enqueue(CurrentIngredient);
            }

            CurrentIngredient = ingredientQueue.Dequeue();
            yield return CurrentIngredient;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

#endregion
    }
}