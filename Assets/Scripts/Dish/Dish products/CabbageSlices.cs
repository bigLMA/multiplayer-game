using UnityEngine;

namespace Dish.DishProducts
{
    public class CabbageSlices : DishProductBase
    {
        public void Destroy()=> Destroy(gameObject);

        public override string GetName() => "cabbage";
    }
}