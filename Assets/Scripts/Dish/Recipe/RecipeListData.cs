using System.Collections.Generic;
using UnityEngine;

namespace Dish.Recipe
{
    public class RecipeListData : ScriptableObject
    {
        public List<RecipeData> recipes;

        public RecipeData GetRecipe()
        {
            return recipes[Random.Range(0, recipes.Count)];
        }
    }
}