using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class StoreMenu
    {
        List<category> menuCats;
        string tableID;

        public string theTableID
        {
            get { return tableID; }
            set { tableID = value; }
        }

        public List<category> theCats
        {
            get { return menuCats; }
            set { menuCats = value; }
        }

        public StoreMenu()
        {

        }
    }
}
