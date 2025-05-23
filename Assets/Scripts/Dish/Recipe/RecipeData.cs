using System.Collections.Generic;
using UnityEngine;

namespace Dish.Recipe
{
    [CreateAssetMenu(fileName ="recipeData", menuName ="Custom/recipeData")]
    public class RecipeData : ScriptableObject
    {
        public string recipeName;

        public List<string> recipe;

        public bool containsMeat;
    }
}