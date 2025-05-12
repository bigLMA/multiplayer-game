using System.Collections.Generic;

namespace Dish
{
    public interface IDish
    {
        Dictionary<string, int> dish { get; }

        void Add(IDishProduct product);

        void Destroy();
    }
}