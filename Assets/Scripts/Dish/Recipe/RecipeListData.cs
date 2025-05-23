using System.Collections.Generic;
using UnityEngine;

namespace Dish.Recipe
{
    [CreateAssetMenu(fileName = "recipeListData", menuName = "Custom/recipeListData")]
    public class RecipeListData : ScriptableObject
    {
        public List<RecipeData> recipes;

        public RecipeData GetRecipe()
        {
            return recipes[Random.Range(0, recipes.Count)];
        }
    }
}