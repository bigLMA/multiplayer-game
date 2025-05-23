using System.Collections;
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

        [SerializeField]
        [Range(4f, 10f)]
        private float recipeAddDelayMin = 5.5f;

        [SerializeField]
        [Range(7f, 15f)]
        private float recipeAddDelayMax = 8f;

        [SerializeField]
        [Range(1f, 3f)]
        private float initialDelay =1.2f;

        public List<RecipeData> recipesAwaiting { get; set; } = new();

        public static RecipeManager instance { get; private set; }

        public int successful { get; private set; } = 0;
        public int failed { get; private set; } = 0;

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

        private void AddWaiting() => recipesAwaiting.Add(recipeList.GetRecipe());

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