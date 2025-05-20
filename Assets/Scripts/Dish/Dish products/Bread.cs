using UnityEngine;

namespace Dish.DishProducts
{
    public class Bread : DishProductBase
{
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public override string GetName() => "bread";
    }
}