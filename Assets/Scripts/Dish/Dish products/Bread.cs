using UnityEngine;

namespace Dish.DishProducts
{
    public class Bread : MonoBehaviour, IDishProduct
{
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public string GetName() => "bread";
    }
}