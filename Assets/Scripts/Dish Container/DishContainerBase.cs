using Dish;
using UnityEngine;

namespace DishContainer
{
    public abstract class DishContainerBase : MonoBehaviour
    {
        public abstract void Add(DishProductBase product);

        public abstract void Clear();

        public IDish dish {  get; protected set; }  
    }
}