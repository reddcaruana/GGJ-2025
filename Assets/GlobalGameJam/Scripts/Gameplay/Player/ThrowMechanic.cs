using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class ThrowMechanic
    {
        private readonly float speed;
        private readonly float angle;

        public ThrowMechanic(float speed, float angle)
        {
            this.speed = speed;
            this.angle = angle;
        }

        public void Drop(Bag bag, Direction direction)
        {
            if (bag.Contents is not IngredientData ingredientData)
            {
                return;
            }

            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
            var ingredient = ingredientManager.Generate(ingredientData, bag.GetAnchor());
            ingredient.Throw(direction.ToVector(), speed, angle);
            
            // TODO: Throw the object
            bag.Clear();
        }
    }
}