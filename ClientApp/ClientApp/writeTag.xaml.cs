using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

namespace ClientApp
{
    public partial class writeTag : PhoneApplicationPage
    {
        private readonly ProximityDevice _proximityDevice;
        private long subId = 0;
        private long pubId = 0;
        public writeTag()
        {
            InitializeComponent();
            _proximityDevice = ProximityDevice.GetDefault();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_proximityDevice != null)
                subId = _proximityDevice.SubscribeForMessage("WriteableTag", OnWriteableTagArrived);

            base.OnNavigatedTo(e);
        }
        private void OnWriteableTagArrived(ProximityDevice sender, ProximityMessage message)
        {
     
            var dataWriter = new DataWriter();
            dataWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
            string appLauncher = string.Format(@"mywaiter:MainPage?source=3");
  
            dataWriter.WriteString(appLauncher);
            pubId = sender.PublishBinaryMessage("WindowsUri:WriteTag", dataWriter.DetachBuffer());
    
        }

    }
}