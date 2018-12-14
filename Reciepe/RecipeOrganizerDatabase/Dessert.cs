using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeOrganizerDatabase
{
   public  class Dessert : Recipe
    {
        public Dessert(int id, string title, string yield, string servingsize, string directions, string comment, string reciepetype) :
          base(id, title, yield, servingsize, directions, comment, reciepetype)
        {
        }
        public Dessert()
        {

        }


        public override string ToString()
        {

            return "D-" + Title;
        }
    }
}
