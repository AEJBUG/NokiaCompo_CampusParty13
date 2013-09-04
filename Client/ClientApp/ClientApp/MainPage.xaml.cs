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

namespace ClientApp
{
    public partial class MainPage : PhoneApplicationPage
    {
       

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //QR or NFC ID found, get menu
            string tableID = "123F";
            //string XMLurl = "http:////10.10.1.105:5000//api//openTag//" + tableID;
            string XMLurl = "http://peelypeel.com/exampleXML.xml";
            WebClient XMLString = new WebClient();
            XMLString.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadXMLStringCompleted);
            XMLString.DownloadStringAsync(new Uri(XMLurl));
        }

        void DownloadXMLStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;
            XDocument doc = XDocument.Parse(e.Result);
            MessageBox.Show(doc.Document.ToString());

            XNode firstChild = doc.FirstNode;
            var rootElement = doc.Elements();

            //create a menu
            StoreMenu theMenu = new StoreMenu();

            //loop though the XML. EXTRACT
            string theCats = "";
            foreach (XElement root in rootElement)
            {//at document root
                theCats += root.Name+"   ";
                var cats = root.Elements();

                //Create Catogry List
                List<category> theCategoriesList = new List<category>();

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
                llDrinks.ItemsSource = theCategoriesList.ToList();

                
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
            
            
            MessageBox.Show(theCats);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

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