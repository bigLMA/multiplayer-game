using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField]
        [Range(8f, 15f)]
        private float recipeAddDelayMin = 10f;

        [SerializeField]
        [Range(12f, 25f)]
        private float recipeAddDelayMax = 22f;

        [SerializeField]
        [Range(1f, 3f)]
        private float initialDelay =1.2f;

        public List<RecipeData> recipesAwaiting { get; set; } = new();

        public static RecipeManager instance { get; private set; }

        public int successful { get; private set; } = 0;
        public int failed { get; private set; } = 0;

        public delegate void CompareHandler();
        public event CompareHandler OnCompareSuccess;
        public event CompareHandler OnCompareFail;

        private void Awake()
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

        private void Start()
        {
            StartCoroutine(AddOrderCoroutine());
        }

        public void CompareDish(IDish dish)
        {
            if (!dish.cooked)
            {
                CompareFailed();
            }

            foreach (var r in recipesAwaiting)
            {
                if (r.recipe.Except(dish.dish).Count() == 0)
                {
                    CompareSuccess(r);
                    return;
                }
            }

            CompareFailed();
        }

        private void CompareSuccess(RecipeData recipe)
        {
            // todo comparing failed
            OnCompareSuccess?.Invoke();
            recipesAwaiting.Remove(recipe);
            //print($"Recipe manager - success");
        }

        private void CompareFailed()
        {
            // todo comparing failed
            OnCompareFail?.Invoke();
            //print($"Recipe manager - fail");
        }

        private void AddWaiting()
        {
            recipesAwaiting.Add(recipeList.GetRecipe());

            print(recipesAwaiting[recipesAwaiting.Count-1].recipeName);
        }

        private IEnumerator AddOrderCoroutine()
        {
            yield return new WaitForSeconds(initialDelay);

            AddWaiting();

            while(true)
            {
                var delay = Random.Range(recipeAddDelayMin, recipeAddDelayMax);
                yield return new WaitForSeconds(delay);
                AddWaiting();
            }
        }
    }
}