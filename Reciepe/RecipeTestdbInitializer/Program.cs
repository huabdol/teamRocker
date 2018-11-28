using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RecipeOrganizerDatabase;

namespace RecipeTestdbInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<RecipesContext>(new RecipesContextInitializer());

            using (RecipesContext context = new RecipesContext())
            {
                List<Recipe> recipes = (from a in context.Recipes
                                        orderby a.Title
                                        select a).ToList();

   
            }
        }
    }
}
