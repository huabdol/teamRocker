using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary
{
    public class SearchLibrary
    {
        public static bool IsKeywordInList(string[] s, List<string> l)
        {
            
            foreach (var x in s)
            {
                if (!string.IsNullOrEmpty(x))
                {
                    foreach (var y in l)
                    {
                        if ((y.ToLower()).Contains(x.ToLower()) && !string.IsNullOrEmpty(y))

                        {
                            return true;
                        }
                    }
                }         
            }
            return false;
        }
    }
}
