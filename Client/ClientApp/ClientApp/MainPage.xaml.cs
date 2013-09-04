using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClientApp.Resources;

using System.Xml;
using System.Xml.Linq;
using System.Windows.Threading;
using Windows.Networking.Proximity;
using Newtonsoft.Json;

namespace ClientApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        string tableID = "123F";
        List<int> order = new List<int>();
        Dictionary<string, List<Dictionary<string, string>>> menu = new Dictionary<string, List<Dictionary<string, string>>>();
        ProximityDevice device = ProximityDevice.GetDefault();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            /*
            if (ProximityDevice.GetDefault() != null)
            {
                MessageBox.Show("NFC present");
            }
            else
            {
                MessageBox.Show("Your phone has no NFC or NFC is disabled");
            }*/



            long subscribedMessageId = device.SubscribeForMessage("Windows.SampleMessage", messageReceivedHandler);
        }

        void messageReceivedHandler(ProximityDevice device, ProximityMessage message)
        {
           MessageBox.Show("Message Received: "+message);  
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to make an order?", "No", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var api = new Hacktakular();
                var data = await api.GetMenu(tableID);
                menu = data;
                // DRINKS FIRST MOTHERFUCKER
                foreach (Dictionary<string, string> d in data["drinks"])
                {
                    llDrinks.ItemsSource.Add(String.Format("{0} - {1}", d["name"], d["desc"]));
                }
                 foreach (Dictionary<string, string> d in data["desert"])
                {
                    llDesert.ItemsSource.Add(String.Format("{0} - {1}", d["name"], d["desc"]));
                }
                foreach (Dictionary<string, string> d in data["starter"])
                {
                    llStarter.ItemsSource.Add(String.Format("{0} - {1}", d["name"], d["desc"]));
                }
                foreach (Dictionary<string, string> d in data["main"])
                {
                    llMain.ItemsSource.Add(String.Format("{0} - {1}", d["name"], d["desc"]));
                }

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            var api = new Hacktakular();
            api.MakeOrder(order, tableID);
        }

        void orderStatus(object sender, DownloadStringCompletedEventArgs e)
        {
            MessageBox.Show("i got a reply about order status thinggy");
        }

        private void llDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Dictionary<string, string> d in menu["drinks"])
            {
                var txt = String.Format("{0} - {1}", d["name"], d["desc"]);
                if (llDrinks.SelectedItem == txt){
                    order.Add(int.Parse(d["id"]));
                }
            }
            updateSummary();
        }

        private void llStarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Dictionary<string, string> d in menu["starter"])
            {
                var txt = String.Format("{0} - {1}", d["name"], d["desc"]);
                if (llStarter.SelectedItem == txt)
                {
                    order.Add(int.Parse(d["id"]));
                }
            } 
        }

        private void llMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Dictionary<string, string> d in menu["main"])
            {
                var txt = String.Format("{0} - {1}", d["name"], d["desc"]);
                if (llMain.SelectedItem == txt)
                {
                    order.Add(int.Parse(d["id"]));
                }
            } 
        }

        private void llDesert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Dictionary<string, string> d in menu["desert"])
            {
                var txt = String.Format("{0} - {1}", d["name"], d["desc"]);
                if (llMain.SelectedItem == txt)
                {
                    order.Add(int.Parse(d["id"]));
                }
            } 
        }

        private void llSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            /*
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (padAndPound("    " + item.theQuantity+" x "+item.theName,item.thePrice) == llSummary.SelectedItem + "")
                    {
                        item.theQuantity--;
                        recentUpdate(item.theName, false);
                    }
                }
            }
            
            updateSummary();
             * */
        }

        public void recentUpdate(string text, bool add)
        {
            if (add)
            {
                toast.Text = "Added " + text + " to order";
            }
            else
            {
                toast.Text = "Removed " + text + " from order";
            }

            // creating timer instance
            DispatcherTimer newTimer = new DispatcherTimer();
            // timer interval specified as 1 second
            newTimer.Interval = TimeSpan.FromMilliseconds(500);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;
            // starting the timer
            newTimer.Start();

            updateSummary();
        }

        List<string> summeryList = new List<string>();
        double FoodCost;
        public void updateSummary()
        {
            List<string> summeryList = new List<string>();
            FoodCost = 0;
            /*
            foreach (category cat in theCategoriesList)
            {
                summeryList.Add(cat.theName);
                int count = 0;
                foreach (Items item in cat.theItems)
                {
                    if (item.theQuantity > 0)
                    {
                        summeryList.Add(padAndPound("    " + item.theQuantity+" x "+item.theName,item.thePrice));
                        count++;
                        FoodCost += item.theQuantity * item.thePrice;
                    }

                }
                if (count == 0)
                {
                    summeryList.Add("    None Selected");
                }
                //summeryList.Add(System.Environment.NewLine);
            }

            TotalCostBFTLabel.Text = "£"+Math.Round(FoodCost, 2).ToString();
            llSummary.ItemsSource = summeryList.ToList();
            */
        }

     

        void OnTimerTick(Object sender, EventArgs args)
        {
            // text box property is set to current system date.
            // ToString() converts the datetime value into text
            toast.Text = "";
        }

        public string padAndPound(string text, float price)
        {
            int len = 50 - text.Length;
            for (int i = 0; i < len; i++)
            {
                text += " ";
            }
            text += "£" + price;
            return text;
        }

        double Tip;
        double finalCost;
        private void TipSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FoodCost == null || FoodCost == 0)
            {
                ThankyouLable.Opacity = 0;
                return;
            }
            
            TipSlider.Maximum = FoodCost * 0.15;
            Tip = TipSlider.Value;
            TipAmountLable.Text = "£" + Math.Round(Tip, 2);
            finalCost = FoodCost+Tip;
            FinalCostLabel.Text = "£" + Math.Round(finalCost, 2);

            ThankyouLable.Opacity = (100*(Tip/TipSlider.Maximum))/80;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//PAY

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}