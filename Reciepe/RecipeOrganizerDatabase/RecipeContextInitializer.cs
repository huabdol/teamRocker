using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Xml.Linq;
using System.Xml;

namespace RecipeOrganizerDatabase
{
    public class RecipesContextInitializer : DropCreateDatabaseIfModelChanges<RecipesContext>
    {
        protected override void Seed(RecipesContext context)
        {
            //base.Seed(context);)

            List<Recipe> recipes = GetRecepiesFromXMLDocument();

            foreach (Recipe recipe in recipes)

            {
                context.Recipes.Add(recipe);
            }
            try
            {
                context.SaveChanges();
            }
            catch(Exception e)
            {

            }

            // Read the ingredients from xml and load to DB

            List<Ingredient> ingredients = GetIngredientsFromXMLDocument();

            foreach (Ingredient ing in ingredients)

            {
                context.Ingredients.Add(ing);
            }

            context.SaveChanges();

        }

        static List<Recipe> GetRecepiesFromXMLDocument()
        {

            var recipeXML = (
                from a in XDocument.Load(@"..\..\..\Recipes.xml").Descendants("Recipe")
                select a).ToList();

            List<Recipe> recipes = new List<Recipe>(recipeXML.Count);

            Recipe recipe = null;

            foreach (var rec in recipeXML)
            {
                recipe = new Recipe();

                recipe.RecipeID = int.Parse(rec.Element("RecipeID").Value);
                recipe.Title = rec.Element("Title").Value;

                recipe.Yield = rec.Element("Yield")?.Value;
                recipe.ServingSize = rec.Element("ServingSize")?.Value;
                recipe.Directions = rec.Element("Directions").Value;
                recipe.Comment = rec.Element("Comment")?.Value;
                recipe.RecipeType = rec.Element("RecipeType").Value;

                recipes.Add(recipe);
            }
            return recipes;
        }


        static List<Ingredient> GetIngredientsFromXMLDocument()
        {
            var ingredientXML = (
                from a in XDocument.Load(@"..\..\..\Ingredients.xml").Descendants("Ingredient")
                select a).ToList();

            List<Ingredient> ingredients = new List<Ingredient>(ingredientXML.Count);

            Ingredient ingredient = null;

            foreach (var ing in ingredientXML)
            {
                ingredient = new Ingredient();

                ingredient.IngredientID = int.Parse(ing.Element("IngredientID").Value);
                ingredient.RecipeID = int.Parse(ing.Element("RecipeID").Value);
                ingredient.Description = ing.Element("Description")?.Value;


                ingredients.Add(ingredient);
            }
            return ingredients;
        }
    }
}
