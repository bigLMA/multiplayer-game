using Dish.Recipe;
using UnityEngine;

namespace Level
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager instance;

        public int recipesSuccessful { get; private set; } = 0;

        public int recipesFailed { get; private set; } = 0;

        void Start()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if(instance != this)
                {
                    Destroy(gameObject);
                    return;
                }
            }

            RecipeManager.instance.OnCompareSuccess += () =>
            {
                ++recipesSuccessful;
            };

            RecipeManager.instance.OnCompareFail += () =>
            {
                ++recipesFailed;
            };
        }
    }
}