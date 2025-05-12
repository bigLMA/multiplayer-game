using UnityEngine;

namespace Dish.DishProducts
{
    public class TomatoSlices : MonoBehaviour, IDishProduct
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public string GetName() => "tomato";
    }
}