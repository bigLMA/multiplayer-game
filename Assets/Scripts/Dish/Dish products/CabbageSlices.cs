using UnityEngine;

namespace Dish.DishProducts
{
    public class CabbageSlices : MonoBehaviour, IDishProduct
    {
        public void Destroy()=> Destroy(gameObject);

        public string GetName() => "cabbage";
    }
}