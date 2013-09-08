using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;

namespace ClientApp
{
    public partial class OrderComplete : PhoneApplicationPage
    {
        DispatcherTimer closeTimer = new DispatcherTimer();

        public OrderComplete()
        {
            InitializeComponent();

            // timer interval specified as 1 second
            closeTimer.Interval = TimeSpan.FromMilliseconds(2000);
            // Sub-routine OnTimerTick will be called at every 1 second
            closeTimer.Tick += closeApp;
            // starting the timer
            closeTimer.Start();
        }

        public void closeApp(Object sender, EventArgs args)
        {
            App.Current.Terminate();
        }
    }
}