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
            bool isKeyWordExist = false;
            foreach (var x in s)
            {
                if (!string.IsNullOrEmpty(x))
                {
                    foreach (var y in l)
                    {
                        if ((y.ToLower()).Contains(x.ToLower()) && !string.IsNullOrEmpty(y))
                        {
                            isKeyWordExist =  true;
                            break;
                        }
                        else {
                            isKeyWordExist = false;
                        }

                    }
                }

            }
            return isKeyWordExist;
        }
    }
}
