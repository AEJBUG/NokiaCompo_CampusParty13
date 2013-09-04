using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ClientApp
{
    public partial class PanoramaPage1 : PhoneApplicationPage
    {
       

        public PanoramaPage1()
        {
            InitializeComponent();

            begin();
        }

        public void begin()
        {
            //List<category> theCatList = (List<category>)Application.Current.ApplicationLifetimeObjects[0];
        }
    }
}