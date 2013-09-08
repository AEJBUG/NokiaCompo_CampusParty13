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
using System.Windows.Media;

namespace ClientApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        string tableID;
        List<category> theCategoriesList = new List<category>();
        ProximityDevice device = ProximityDevice.GetDefault();
        DispatcherTimer dwindleTimer = new DispatcherTimer();
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

           
            // timer interval specified as 1 second
            dwindleTimer.Interval = TimeSpan.FromMilliseconds(250);
            // Sub-routine OnTimerTick will be called at every 1 second
            dwindleTimer.Tick += DwindleTimerMethod;
            // starting the timer




            //if (ProximityDevice.GetDefault() != null)
            //    MessageBox.Show("NFC present");
            //else
            //    MessageBox.Show("Your phone has no NFC or NFC is disabled");




            long subscribedMessageId = device.SubscribeForMessage("Windows.SampleMessage", messageReceivedHandler);
        }

        void messageReceivedHandler(ProximityDevice device, ProximityMessage message)
        {
           MessageBox.Show("Message Received: "+message);  
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    String value;
        //    if (NavigationContext.QueryString.TryGetValue("ms_nfp_launchargs", out value))
        //    {
        //        MessageBox.Show(value);
        //    }
            

        //}

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";
            string butt="false";
            NavigationContext.QueryString.TryGetValue("butt", out butt);
            bool tmp;
            bool.TryParse(butt, out tmp);
            if (tmp)
            {
                NavigationContext.QueryString.TryGetValue("msg", out tableID);
            }
            else
            {
                NavigationContext.QueryString.TryGetValue("source", out tableID);
            }

            
            

            //QR or NFC ID found, get menu
            //tableID = "123F";
            string XMLurl;
            WebClient XMLString;
            if (MessageBox.Show("Would you like to make an order for table "+tableID+"?", "You have selected table "+tableID, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //string XMLurl = "http:////10.10.1.105:5000//api//openTag//" + tableID;
                XMLurl = "http://ordernow.cloudapp.net:5000/api/start/"+tableID;
                XMLString = new WebClient();
                XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadXMLStringCompleted2);
                XMLString.DownloadStringAsync(new Uri(XMLurl));
            }
            else
            {
                NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
            }

            
           

        } 

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I figuratively do nothing");
            ////QR or NFC ID found, get menu
            ////tableID = "123F";
            //string XMLurl;
            //WebClient XMLString;
            //if (MessageBox.Show("Would you like to make an order?", "No", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //    MessageBox.Show("YOU CLICKED YES");
            //    XMLurl = "http://10.10.1.105:5000/api/startsession/" + tableID;
            //    XMLString = new WebClient();
            //    XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ignoreReply);
            //    XMLString.DownloadStringAsync(new Uri(XMLurl));
            //}
            //else
            //{
            //    NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
            //}

            ////string XMLurl = "http:////10.10.1.105:5000//api//openTag//" + tableID;
            //XMLurl = "http://peelypeel.com/exampleXML.xml";
            //XMLString = new WebClient();
            //XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadXMLStringCompleted2);
            //XMLString.DownloadStringAsync(new Uri(XMLurl));
        }

        void ignoreReply(object sender, DownloadStringCompletedEventArgs e)
        {

        }

        void DownloadXMLStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;
            XDocument doc = XDocument.Parse(e.Result);
            var cats = doc.Descendants("category");
            foreach (var category in cats)
            {
                int count = 0;
                foreach (category catry in theCategoriesList)
                {
                    if (catry.theName == category.Value)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    theCategoriesList.Add(new category(category.Value));
                }
            }

            int yes = theCategoriesList.Count;

            //now add items to right categori
            var items = doc.Descendants("item");
            
            foreach (category catry in theCategoriesList)
            {
                List<Items> IList = new List<Items>();
                foreach (var item in items)
                {
                    XElement catergory = (XElement)item.FirstNode.NextNode;
                    string catgory = catergory.Value;
                    if(catgory == catry.theName)
                    {//PROCEED
                        Items tmpItem = new Items();


                        XElement IDEl = (XElement)item.FirstNode;
                        int id = int.Parse(IDEl.Value);
                        tmpItem.theID = id;

                        XElement Price = (XElement)item.FirstNode.NextNode.NextNode;
                        float price = float.Parse(Price.Value);
                        tmpItem.thePrice = price;

                        XElement Name = (XElement)item.FirstNode.NextNode.NextNode.NextNode;
                        string name = Name.Value;
                        tmpItem.theName = name;

                        XElement Des = (XElement)item.FirstNode.NextNode.NextNode.NextNode.NextNode;
                        string des = Des.Value;
                        tmpItem.theDescription = des;

                        IList.Add(tmpItem);
                    }
                   
                }
                catry.theItems = IList;
            }

            yes = theCategoriesList.Count;


            int count2 = 0;
            foreach (category TMPCAT in theCategoriesList)
            {
                List<string> tmpstringlist = new List<string>();
                foreach (Items tmpitems in TMPCAT.theItems)
                {
                    tmpstringlist.Add("+ " + tmpitems.theName);
                    //tmpstringlist.Add("   " + tmpitems.theDescription);
                }

                switch (count2)
                {
                    case 0:
                        llStarter.ItemsSource = tmpstringlist.ToList();
                        break;
                    case 1:
                        llDesert.ItemsSource = tmpstringlist.ToList();
                        break;
                    case 2:
                        llMain.ItemsSource = tmpstringlist.ToList();
                        break;
                    case 3:
                        llDrinks.ItemsSource = tmpstringlist.ToList();
                        break;
                }

                count2++;
            }

            updateSummary();
        }

        void DownloadXMLStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            XDocument doc = XDocument.Parse(e.Result);
            //MessageBox.Show(doc.Document.ToString());

            XNode firstChild = doc.FirstNode;
            var rootElement = doc.Elements();

            //create a menu
            StoreMenu theMenu = new StoreMenu();
            theMenu.theTableID = tableID;

            //loop though the XML. EXTRACT
            string theCats = "";
            foreach (XElement root in rootElement)
            {//at document root
                theCats += root.Name+"   ";
                var cats = root.Elements();

                //Create Catogry List
                //moved to top to make access in this class everywqhere
                
                //at each catagory
                foreach (XElement cat in cats)
                {//for each catgory          
                    theCats += cat.Attribute("Name").Value + "      ";

                    //add categories name to list
                    category tmpCat = new category(cat.Attribute("Name").Value);

                    //get teh childen (items)
                    var catItems = cat.Elements();
                    List<Items> itemsList = new List<Items>();
                    foreach (XElement item in catItems)
                    {
                        Items tmpItem = new Items(item.Attribute("Name").Value, item.Attribute("Desciption").Value, item.Attribute("Price").Value, item.Attribute("ID").Value);
                        itemsList.Add(tmpItem);
                        theCats += item.Attribute("Name").Value + " ";
                    }
                    tmpCat.theItems = itemsList;

                    theCategoriesList.Add(tmpCat);

                }
                int lawlfake = theCategoriesList.Count;

                //Application.Current.ApplicationLifetimeObjects.Add(theCategoriesList);

                //NavigationService.Navigate(new Uri("/PanoramaPage1.xaml", UriKind.Relative));

                //ObjectNav.NavigationExtensions.Navigate(this, "/PanoramaPage1.xaml", theCategoriesList);

              

                //List<AlphaKeyGroup<category>> DataSource = AlphaKeyGroup<category>.CreateGroups(theCategoriesList,
                //System.Threading.Thread.CurrentThread.CurrentUICulture,
                //(category s) => { return s.theName; }, true);

                //catLongList.ItemsSource = DataSource;
                //theCats += cat.FirstAttribute.Value;

                //populate pano with menu
                //List<string> tmpstringlist = new List<string>();
                int count=0;
                foreach (category TMPCAT in theCategoriesList)
                {
                    List<string> tmpstringlist = new List<string>();
                    foreach (Items tmpitems in TMPCAT.theItems)
                    {
                        tmpstringlist.Add("+ "+tmpitems.theName);
                        tmpstringlist.Add("   " + tmpitems.theDescription);
                    }

                    switch (count)
                    {
                        case 0:
                            llDrinks.ItemsSource = tmpstringlist.ToList();
                            break;
                        case 1:
                            llStarter.ItemsSource = tmpstringlist.ToList();
                            break;
                        case 2:
                            llMain.ItemsSource = tmpstringlist.ToList();
                            break;
                        case 3:
                            llDesert.ItemsSource = tmpstringlist.ToList(); 
                            break;
                    }

                    count++;
                }


              
             
                

                
                //foreach (category cat in theCategoriesList)
                //{
                //    panoPaneDrinks.Header = cat.theName;
                //    foreach (Items item in cat.theItems)
                //    {
                //        List<AlphaKeyGroup<Items>> DataSource = AlphaKeyGroup<Items>.CreateGroups(cat.theItems,
                //        System.Threading.Thread.CurrentThread.CurrentUICulture,
                //        (Items s) => { return s.theName; }, true);

                        

                //    }
                //}
            }
            
            
            //MessageBox.Show(theCats);
            updateSummary();

        }

        Windows.Networking.Proximity.ProximityDevice proximityDevice;
        private void PublishLaunchApp()
        {
            proximityDevice = Windows.Networking.Proximity.ProximityDevice.GetDefault();

            if (proximityDevice != null)
            {
                // The format of the app launch string is: "<args>\tWindows\t<AppName>".
                // The string is tab or null delimited.

                // The <args> string can be an empty string ("").
                string launchArgs = "user=default";

                // The format of the AppName is: PackageFamilyName!PRAID.
                string praid = "{b8c21b6b-2f16-49f6-9ee9-b3a713c54500}"; // The Application Id value from your package.appxmanifest.

                string appName = Windows.ApplicationModel.Package.Current.Id.FamilyName + "!" + praid;

                string launchAppMessage = launchArgs + "\tWindows\t" + appName;

                var dataWriter = new Windows.Storage.Streams.DataWriter();
                dataWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
                dataWriter.WriteString(launchAppMessage);
                var launchAppPubId =
                proximityDevice.PublishBinaryMessage(
                    "LaunchApp:WriteTag", dataWriter.DetachBuffer());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PublishLaunchApp();
            return;
            //contruct URL to save stuff
            string baseURL = "http://10.10.1.105:5000/api/placeOrder/";

            //add TableNumber
            baseURL += "?tID=" + tableID;

            //loop over and add all items taht ahve a quantity mroe than 0
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (item.theQuantity > 0)
                    {
                        baseURL += "&" + item.theID + "=" + item.theQuantity;
                    }
                }
            }

            MessageBox.Show(baseURL);

            string XMLurl = baseURL;
            WebClient XMLString = new WebClient();
            XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(orderStatus);
            XMLString.DownloadStringAsync(new Uri(XMLurl));
        }

        void orderStatus(object sender, DownloadStringCompletedEventArgs e)
        {
            MessageBox.Show("i got a reply about order status thinggy");
        }

        private void llDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (("+ " + item.theName) == llDrinks.SelectedItem + "")
                    {
                        item.theQuantity++;
                        recentUpdate(item.theName, true);
                    }
                }
            }
        }

        private void llStarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (("+ " + item.theName) == llStarter.SelectedItem + "")
                    {
                        item.theQuantity++;
                        recentUpdate(item.theName, true);
                    }
                }
            }
        }

        private void llMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (("+ " + item.theName) == llMain.SelectedItem + "")
                    {
                        item.theQuantity++;
                        recentUpdate(item.theName, true);
                    }
                }
            }
        }

        private void llDesert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (("+ "+item.theName) == llDesert.SelectedItem + "")
                    {
                        item.theQuantity++;
                        recentUpdate(item.theName, true);
                    }
                }
            }
        }

        private void llSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (padAndPound("  - " + item.theQuantity+" x "+item.theName,item.thePrice) == llSummary.SelectedItem + "")
                    {
                        item.theQuantity--;
                        recentUpdate(item.theName, false);
                    }
                }
            }
            
            updateSummary();
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
            newTimer.Interval = TimeSpan.FromMilliseconds(2500);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;
            // starting the timer
            newTimer.Start();

            updateSummary();
        }

        List<string> summeryList = new List<string>();
        float FoodCost;
        public void updateSummary()
        {
            List<string> summeryList = new List<string>();
            FoodCost = 0;
           
            foreach (category cat in theCategoriesList)
            {
                summeryList.Add(cat.theName);
                int count = 0;
                foreach (Items item in cat.theItems)
                {
                    if (item.theQuantity > 0)
                    {
                        summeryList.Add(padAndPound("  - " + item.theQuantity+" x "+item.theName,item.thePrice));
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

            TotalCostBFTLabel.Text = "£"+Math.Round(FoodCost, 2).ToString("0.00");
            llSummary.ItemsSource = summeryList.ToList();
            
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
            
            //TipSlider.Maximum = FoodCost * 0.15;
            //Tip = TipSlider.Value;
            //TipAmountLable.Text = "£" + Math.Round(Tip, 2);
            //finalCost = FoodCost+Tip;
            //FinalCostLabel.Text = "£" + Math.Round(finalCost, 2);

            //ThankyouLable.Opacity = 2+(100*(Tip/TipSlider.Maximum))/90;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//PAY *cough* i mean order
            foreach (category cat in theCategoriesList)
            {
                foreach (Items item in cat.theItems)
                {
                    if (item.theQuantity > 0)
                    {
                        string orderURL = giveMeAURLForThisItemAndQuantityOrderBoy(tableID, item.theID, item.theQuantity);
                        string XMLurl;
                        WebClient XMLString;
                        XMLurl = orderURL;
                        XMLString = new WebClient();
                        XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ignoreReply);
                        XMLString.DownloadStringAsync(new Uri(XMLurl));

                        item.theQuantity = 0;
                    }
                }
            }
            dwindle();
        }

        public string giveMeAURLForThisItemAndQuantityOrderBoy(string table, int itemID, int quantiy)
        {
            string ret = "http://ordernow.cloudapp.net:5000/api/order/"+table+"/"+itemID+"/"+quantiy;
            return ret;
        }

        public void dwindle()
        {
            dwindleTimer.Start();
        }

        int timerLength = 25;
        int loopCount = 0;
        float downTick = 0;
        public void DwindleTimerMethod(Object sender, EventArgs args)
        {
            switch (loopCount)
            {
                case 0:
                    downTick = FoodCost / 25;
                    break;
                case 25:
                    loopCount = 0;
                    dwindleTimer.Stop();
                    timerLength = 250;
                    NavigationService.Navigate(new Uri("/OrderComplete.xaml", UriKind.Relative));
                    return;
                    break;
            }
        
            loopCount++;
            timerLength -= 2;
            FoodCost -= downTick;
            dwindleTimer.Interval = TimeSpan.FromMilliseconds(timerLength);
            TotalCostBFTLabel.Text = padMe(FoodCost);

        }

        public string padMe(float price)
        {
            string ret = "£" + Math.Round(price, 2).ToString("0.00");
            string[] penny = ret.Split('.');
            if (penny[1].Length == 1)
            {
                ret += "0";
            }
            return ret;
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