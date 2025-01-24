namespace GlobalGameJam.Gameplay
{
    public class Throw
    {
        private readonly float speed;
        private readonly float angle;

        public Throw(float speed, float angle)
        {
            this.speed = speed;
            this.angle = angle;
        }

        public void Drop(Bag bag, Direction direction)
        {
            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
            
            var ingredient = ingredientManager.Generate(bag.Contents, bag.GetAnchor());
            ingredient.Launch(direction.ToVector(), speed, angle);
            
            // TODO: Throw the object
            bag.Clear();
        }
    }
}