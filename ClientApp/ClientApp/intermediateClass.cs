using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class intermediateClass
    {
        int ID;
        string Cat, Na, De;
        float pr;

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public string Catergory
        {
            get { return Cat; }
            set { Cat = value; }
        }

        public string Name
        {
            get { return Na; }
            set { Na = value; }
        }

        public string Description
        {
            get { return De; }
            set { De = value; }
        }

        public float Price
        {
            get { return pr; }
            set { pr = value; }
        }


        public intermediateClass(int id, string category, string name, string description, string price)
        {
            ID = id;
            category = Cat;
            Na = name;
            De = description;
            float.TryParse(price, out pr);
        }

        public intermediateClass()
        {
           
        }
    }
}
