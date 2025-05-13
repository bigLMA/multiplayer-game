using System.Collections.Generic;
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

        public Dictionary<string, int> dish { get; private set; } = new();

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

            dish[product.GetName()] = dish.ContainsKey(product.GetName()) ? dish[product.GetName()] + 1 : 1;

            if (product.GetName().Equals("meat"))
            {
                //TODO meat add


                rawMeat.SetActive(true);
                Destroy(product.gameObject);
                return;
            }

            if (dish[product.GetName()] ==1)
            {
                dishMap[product.GetName()].SetActive(true);  
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
