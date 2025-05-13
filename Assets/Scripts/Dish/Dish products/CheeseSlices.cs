using UnityEngine;

namespace Dish.DishProducts
{
    public class CheeseSlices : DishProductBase
    {
        public void Destroy()=> Destroy(gameObject);

        public override string GetName() => "cheese";
    }
}