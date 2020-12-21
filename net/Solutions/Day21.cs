using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day21 : BaseDay
    {
        public override string SolveA()
        {
            var recipes = GetRecipes();
            var ingredients = recipes.SelectMany(x => x.Ingredients).ToList();
            return GetSafeIngredients(recipes).Sum(safeIngredient => ingredients.Count(ingredient => ingredient == safeIngredient)).ToString();
        }

        public override string SolveB()
        {
            var recipes = GetRecipes();
            var ingredients = recipes.SelectMany(x => x.Ingredients).ToList();
            var allergens = recipes.SelectMany(x => x.Allergens).Distinct().ToList();
            var safeIngredients = GetSafeIngredients(recipes);

            recipes = recipes.Select(recipe => (recipe.Ingredients!.Except(safeIngredients).ToList(), recipe.Item2)).ToList();
            
            var identifiedAllergens = new Dictionary<string, string>();
            while (identifiedAllergens.Count != allergens.Count)
            {
                foreach (var allergen in allergens.Except(identifiedAllergens.Keys))
                {
                    var possibleIngredients = ingredients.Except(safeIngredients).Except(identifiedAllergens.Values).ToList();
                    foreach (var recipe in recipes.Where(recipe => recipe.Allergens!.Contains(allergen)))
                    {
                        possibleIngredients = possibleIngredients.Intersect(recipe.Ingredients).ToList();
                    }

                    if (possibleIngredients.Count == 1)
                    {
                        identifiedAllergens[allergen] = possibleIngredients.Single();
                    }
                }
            }

            return string.Join(",", identifiedAllergens.OrderBy(x => x.Key).Select(x => x.Value));
        }

        private List<(List<string> Ingredients, List<string> Allergens)> GetRecipes()
        {
            var recipes = new List<(List<string>, List<string>)>();
            foreach (var line in lines)
            {
                var split = line.Split(" (");
                recipes.Add((split[0].Split().ToList(), split[1].TrimEnd(')')[9..].Split(", ").ToList()));
            }

            return recipes;
        }

        private List<string> GetSafeIngredients(List<(List<string> Ingredients, List<string> Allergens)> recipes)
        {
            var ingredients = recipes.SelectMany(x => x.Ingredients).ToList();
            var allergens = recipes.SelectMany(x => x.Allergens).Distinct().ToList();
            var badIngredients = new List<string>();

            foreach (var allergen in allergens)
            {
                var possibleIngredients = ingredients.Distinct().ToList();
                foreach (var recipe in recipes)
                {
                    if (recipe.Allergens.Contains(allergen))
                    {
                        possibleIngredients = possibleIngredients.Intersect(recipe.Ingredients).ToList();
                    }
                }
                badIngredients = badIngredients.Union(possibleIngredients).ToList();
            }

            return ingredients.Except(badIngredients).ToList();
        }
    }
}