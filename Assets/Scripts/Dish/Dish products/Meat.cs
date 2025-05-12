using UnityEngine;

namespace Dish.DishProducts
{
    public class Meat : MonoBehaviour, IDishProduct
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public string GetName() => "meat";
    }
}