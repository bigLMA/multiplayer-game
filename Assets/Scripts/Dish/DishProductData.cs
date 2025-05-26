using UnityEngine;

namespace Dish
{
    [CreateAssetMenu(fileName = "dishProductData", menuName = "Custom/dishProductData")]
    public class DishProductData : ScriptableObject
    {
        public string productName;
        public Sprite image;
    }
}