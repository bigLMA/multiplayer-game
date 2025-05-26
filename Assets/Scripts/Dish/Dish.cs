using System.Collections.Generic;

namespace Dish
{
    public interface IDish
    {
        List<DishProductData> dish { get; }

        void Add(DishProductBase product);

        void Destroy();

        bool cooked { get; }
    }
}