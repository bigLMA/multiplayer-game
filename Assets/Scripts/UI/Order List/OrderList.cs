using Dish.Recipe;
using UnityEngine;

namespace UI.OrderList
{
    public class OrderList : MonoBehaviour
    {
        [SerializeField]
        private GameObject orderItemPrefab;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            RecipeManager.instance.OnRecipeAdd += OnRecipeAdded;
            RecipeManager.instance.OnRecipeRemove += OnRecipeRemoved;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnRecipeAdded(RecipeData data)
        {
            // Добавить в конец айтем
            var orderItem = Instantiate(orderItemPrefab, transform);
            
            // Передать ему инфу
        }

        private void OnRecipeRemoved(RecipeData data)
        {
            // Убрать первый найденный айтем со схожей датой

            // Перерисовать (зарепарентить) всю таблицу, либо саб систему
        }
    }
}
