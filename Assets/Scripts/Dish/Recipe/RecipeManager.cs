using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Dish.Recipe
{
    /// <summary>
    /// Responsible for storing recipes, comparing with player dishes 
    /// and counting both successful and wrong player dishes
    /// </summary>
    public class RecipeManager : MonoBehaviour
    {
        [SerializeField]
        private RecipeListData recipeList;

        private List<RecipeData> recipesAwaiting = new();

        public static RecipeManager instance { get; private set; }

        public int successful { get; private set; } = 0;
        public int failed { get; private set; } = 0;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void CompareDish(IDish dish)
        {
            if(!dish.cooked)
            {
                OnCompareFailed();
            }

            foreach(var r in recipesAwaiting)
            {
                if (r.recipe.Except(dish.dish).Count()==0)
                {
                    OnCompareSuccess();
                    return;
                }
            }

            OnCompareFailed();
        }

        private void OnCompareSuccess()
        {
            // todo comparing failed
            print($"Recipe manager - success");
        }

        private void OnCompareFailed()
        {
            // todo comparing failed
            print($"Recipe manager - fail");
        }
    }
}