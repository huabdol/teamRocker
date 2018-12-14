using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeOrganizerDatabase;

namespace Reciepe
{
    public class MealItem: Recipe
    {


        public MealItem(int id, string title, string yield, string servingsize, string directions, string comment, string reciepetype):
            base(id,title,yield,servingsize,directions,comment,reciepetype)
        {
        }
        public MealItem()
        {

        }
      

        public override string ToString()
        {
                                 
            return "M-" + Title;
        }

    }
}
