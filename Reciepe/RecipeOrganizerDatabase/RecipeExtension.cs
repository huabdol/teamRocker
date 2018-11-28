using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeOrganizerDatabase;

namespace RecipeOrganizerDatabase
{
    public partial class Recipe
    {
        public override string ToString()
        {
            return Title;
        }
    }
}
