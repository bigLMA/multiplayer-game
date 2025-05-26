using Attach;
using Dish;
using UI.Plate;
using Unity.VisualScripting;
using UnityEngine;

namespace DishContainer
{
    public class Plate : DishContainerBase
    {
        [Header("Dish")]
        [SerializeField]
        [Tooltip("Dish prefab")]
        private GameObject dishPrefab;

        [SerializeField]
        private PlateDisplay display;

        void Awake()
        {
            var attachComp = GetComponent<IAttach>();
            var dishGO = Instantiate(dishPrefab);

            dish = dishGO.GetComponent<IDish>();
            attachComp.Attach(dishGO);
        }

        public override void Add(DishProductBase product)
        {
            if (dish != null)
            {
                dish.Add(product);
                display?.AddProduct(product.productData.image);
            }
        }

        public override void Clear()
        {
            if (dish != null)
            {
                dish.Destroy();
            }
        }
    }
}
