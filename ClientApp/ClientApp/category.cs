using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class category
    {
        string name;
        List<Items> catItems;

        public string theName
        {
            get { return name; }
            set { name = value; }
        }

        public List<Items> theItems
        {
            get { return catItems; }
            set { catItems = value; }
        }

        public category(string Name)
        {
            name = Name;
        }
        public category()
        {
            
        }
    }
}
