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
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string TableID = TableIDText.Text;
            string[] stringArray = TableID.Split(';');
            if (stringArray.Length > 1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/MainPage.xaml?msg=" + TableID+"&butt=true", UriKind.Relative));

        }

        
        int tapCount = 0;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            tapCount++;
            if (tapCount > 10)
            {
                tapCount = 0;
                NavigationService.Navigate(new Uri("/writeTag.xaml", UriKind.Relative));
            }
        }
    }
}