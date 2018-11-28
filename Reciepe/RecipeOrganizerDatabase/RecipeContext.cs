using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RecipeOrganizerDatabase
{
    public partial class RecipesContext: DbContext
    {
        public RecipesContext() : base("RecipesCAContext") { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
