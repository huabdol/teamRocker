using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Added to use Attributes to define primary key, column constraints, etc.
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeOrganizerDatabase
{
    public partial class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        [Required, MaxLength(50), Index(IsUnique =true)]
        public string Title { get; set; }
        
        public string Yield { get; set; }
        [MaxLength(300)]
        public string ServingSize { get; set; }
        [Required]
        public string Directions { get; set; }
        public string Comment { get; set; }
        [Required, MaxLength(30)]
        public string RecipeType { get; set; }

    }
}
