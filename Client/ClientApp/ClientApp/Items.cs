using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Items
    {
        int ID;
        string name, description;
        float price;

        public int theID
        {
            get { return ID; }
            set { ID = value; }
        }

        public string theName
        {
            get { return name; }
            set { name = value; }
        }

        public string theDescription
        {
            get { return description; }
            set { description = value; }
        }

        public float thePrice
        {
            get { return price; }
            set { price = value; }
        }

        public Items(string Name, string Description, string Price, string id)
        {
            name = Name;
            description = Description;
            float.TryParse(Price, out price);
            int.TryParse(id, out ID);
        }
    }
}
