using Dish.FryProducts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dish
{
    public class DishImplementation : MonoBehaviour, IDish
    {
        [Header("Dish Products")]
        [SerializeField]
        [Tooltip("Bread GO")]
        private GameObject bread;

        [SerializeField]
        [Tooltip("Raw Meat GO")]
        private GameObject rawMeat;

        [SerializeField]
        [Tooltip("Cooked Meat GO")]
        private GameObject cookedMeat;

        [SerializeField]
        [Tooltip("Burned Meat GO")]
        private GameObject burnedMeat;

        [SerializeField]
        [Tooltip("Tomato GO")]
        private GameObject tomato;

        [SerializeField]
        [Tooltip("Cheese GO")]
        private GameObject cheese;

        [SerializeField]
        [Tooltip("Cabbage GO")]
        private GameObject cabbage;

        public List<DishProductData> dish { get; private set; } = new();

        public bool cooked => cookedMeat.activeInHierarchy;

        private Dictionary<string, GameObject> dishMap;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            dishMap = new Dictionary<string, GameObject>()
            {
                ["bread"] = bread,
                ["tomato"] = tomato,
                ["cheese"] = cheese,
                ["cabbage"] = cabbage,
            };
        }

        public void Add(DishProductBase product)
        {
            if (product == null) return;

            bool contains = dish.Contains(product.productData);

            //dish[product.GetName()] = dish.ContainsKey(product.GetName()) ? dish[product.GetName()] + 1 : 1;
            dish.Add(product.productData);

            if (product.productData.productName.Equals("meat"))
            {
                if(product.TryGetComponent(out FryProductBase fryProduct))
                {
                    if(fryProduct.state == fryProduct.rawState)
                    {
                        rawMeat.SetActive(true);
                    }

                    if (fryProduct.state == fryProduct.cookedState)
                    {
                        cookedMeat.SetActive(true);
                    }

                    if (fryProduct.state == fryProduct.burnedState)
                    {
                        burnedMeat.SetActive(true);
                    }

                    Destroy(product.gameObject);
                    return;
                }
            }

            //if (dish[product.GetName()] ==1)
            if(!contains)
            {
                dishMap[product.productData.productName].SetActive(true);  
            }

            Destroy(product.gameObject);
        }

        public void Destroy()
        {
            dish.Clear();

            bread.SetActive(false);
            rawMeat?.SetActive(false);
            cabbage?.SetActive(false);
            cookedMeat?.SetActive(false);
            bread.SetActive(false);
            burnedMeat?.SetActive(false);
            cheese.SetActive(false);
            tomato.SetActive(false);
        }
    }
}
