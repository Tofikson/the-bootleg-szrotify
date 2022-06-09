using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace the_meme_generator
{
    public class Listy
    {
        public static List<users> GetUsers() 
        {
            var list = new List<users>();
            using (var db = new DatabaseContext())
            {
                foreach (var pUSSR in db.Users)
                {
                    list.Add(pUSSR);
                }
                return list;
            }
        }
       
    }
}
