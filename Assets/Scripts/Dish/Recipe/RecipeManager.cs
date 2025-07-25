using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Dish.Recipe
{
    /// <summary>
    /// Responsible for storing recipes, comparing with player dishes 
    /// and counting both successful and wrong player dishes
    /// </summary>
    public class RecipeManager : NetworkBehaviour
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

        public delegate void CompareHandler();
        public event CompareHandler OnCompareSuccess;
        public event CompareHandler OnCompareFail;

        public delegate void RecipeHandler(RecipeData data);
        public event RecipeHandler OnRecipeAdd;
        public event RecipeHandler OnRecipeRemove;

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
            NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientId, sceneName, loadSceneMode) =>
            {
                if (IsServer)
                {
                    StartCoroutine(AddOrderCoroutine());
                }
            };
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
            OnCompareSuccess?.Invoke();
            OnRecipeRemove?.Invoke(recipe);
            recipesAwaiting.Remove(recipe);
        }

        private void CompareFailed()
        {
            OnCompareFail?.Invoke();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void AddWaitingRpc(int rand)
        {
            var newRecipe = recipeList.recipes[rand];
            OnRecipeAdd?.Invoke(newRecipe);
            recipesAwaiting.Add(newRecipe);
        }

        private IEnumerator AddOrderCoroutine()
        {
            yield return new WaitForSeconds(initialDelay);

            var rand = Random.Range(0, recipeList.recipes.Count);
            AddWaitingRpc(rand);

            while(true)
            {
                var delay = Random.Range(recipeAddDelayMin, recipeAddDelayMax);
                yield return new WaitForSeconds(delay);
                rand = Random.Range(0, recipeList.recipes.Count);
                AddWaitingRpc(rand);
            }
        }
    }
}