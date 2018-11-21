using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Added to use Attributes to define primary key, column constraints, etc.
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeOrganizerDatabase
{
    public class Ingredient
    {
        [Key, Column(Order =0)]
        public int IngredientID { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Required, Column(Order = 1)]
        public int RecipeID { get; set; }

        //represents foreign keys - sets the relationships between the tables.

        public virtual Recipe Recipe { get; set; }
    }
}
