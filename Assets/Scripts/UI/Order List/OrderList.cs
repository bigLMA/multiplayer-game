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

        private void OnRecipeAdded(RecipeData data)
        {
            var orderItem = Instantiate(orderItemPrefab, transform);
            var orderItemBehaviour = orderItem.GetComponent<OrderListItem>();
            children.Add(orderItemBehaviour);

            orderItemBehaviour.AddOrder(data);
        }

        private void OnRecipeRemoved(RecipeData data)
        {
            if(children.Count>0)
            {
                var itemToRemove = children.FirstOrDefault(item => item.orderName.text.Equals(data.recipeName));

                if (itemToRemove == null) return;

                children.Remove(itemToRemove);

                itemToRemove.transform.parent = null;
                Destroy(itemToRemove.gameObject);
            }
        }
    }
}
