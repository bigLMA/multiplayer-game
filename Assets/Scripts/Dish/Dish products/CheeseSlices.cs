using UnityEngine;

namespace Dish.DishProducts
{
    public class CheeseSlices : MonoBehaviour, IDishProduct
    {
        public void Destroy()=> Destroy(gameObject);

        public string GetName() => "cheese";
    }
}