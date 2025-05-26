using UnityEngine;

namespace Dish
{
    public class DishProductBase : MonoBehaviour
    {
        [field:SerializeField]
        public DishProductData productData { get; private set; }

        public void SetProductData(DishProductData productData) =>this.productData = productData;
    }
}