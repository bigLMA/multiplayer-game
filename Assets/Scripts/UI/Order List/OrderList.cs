using Dish.Recipe;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.OrderList
{
    public class OrderList : MonoBehaviour
    {
        [SerializeField]
        private GameObject orderItemPrefab;

        private List<OrderListItem> children = new();

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
            var orderItemBehaviour = orderItem.GetComponent<OrderListItem>();
            children.Add(orderItemBehaviour);

            // Передать ему инфу
            orderItemBehaviour.AddOrder(data);
        }

        private void OnRecipeRemoved(RecipeData data)
        {
            if(children.Count>0)
            {
                // Убрать первый найденный айтем со схожей датой
                var itemToRemove = children.FirstOrDefault(item => item.orderName.text.Equals(data.recipeName));

                if (itemToRemove == null) return;

                children.Remove(itemToRemove);

                // Перерисовать (зарепарентить) всю таблицу, либо саб систему
                itemToRemove.transform.parent = null;
                Destroy(itemToRemove.gameObject);
            }
        }
    }
}
