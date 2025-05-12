using Attach;
using Dish;
using UnityEngine;

namespace DishContainer
{
    public class Plate : MonoBehaviour, IDishContainer
    {
        [Header("Dish")]
        [SerializeField]
        [Tooltip("Dish prefab")]
        private GameObject dishPrefab;

        private IDish dish;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var attachComp = GetComponent<IAttach>();
            var dishGO = Instantiate(dishPrefab);

            dish = dishGO.GetComponent<IDish>();
            attachComp.Attach(dishGO);
        }

        public void Add(IDishProduct product)
        {
            if(dish!=null)
            {
                dish.Add(product);
            }
        }

        public void Destroy()
        {
            if(dish!=null)
            {
                 dish.Destroy();
            }

            Destroy(gameObject);
        }

        public void DestroyDish()
        {
            if(dish!=null)
            {
                dish.Destroy();
            }
        }
    }
}
