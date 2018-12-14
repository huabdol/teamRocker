using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeOrganizerDatabase
{
    public class Recipees
    {

        internal readonly int NumofRecipes;

        private List<Recipe> recipes;

       internal Recipees(int numofRecipes)
        {
            NumofRecipes = numofRecipes;
            recipes = new List<Recipe>(NumofRecipes);
        }

        public Recipe this[int index]
        {
            get
            {
                Recipe rcp = null;

                if (index >= 0 && index < recipes.Count)
                {
                    //emp = (Employee)employees[index];
                    rcp = recipes[index];
                }

                return rcp;
            }
            private set
            {
                recipes.Add(value);
            }
        }
    }
}
