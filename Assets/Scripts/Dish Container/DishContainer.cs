using System.Collections.Generic;

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

public interface IDishProduct
{
     string name { get; }
}

public interface IDish
{
     Dictionary<string, int> dish { get; }

     void Add(IDishProduct product);

     void Destroy();
}