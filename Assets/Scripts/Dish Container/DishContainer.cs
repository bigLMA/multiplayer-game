using Dish;

namespace DishContainer
{
    public interface IDishContainer
    {
        void Add(IDishProduct product);

        void DestroyDish();

        /// <summary>
        /// Destroy self
        /// </summary>
        void Destroy();
    }
}