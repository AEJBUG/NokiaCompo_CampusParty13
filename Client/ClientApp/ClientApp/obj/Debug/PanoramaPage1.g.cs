﻿#pragma checksum "C:\Users\ITR\Documents\GitHub\NokiaCompo_CampusParty13\Client\ClientApp\ClientApp\PanoramaPage1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "10BAEB35C474B86EA9BCCB58F8A6DCFF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18213
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ClientApp {
    
    
    public partial class PanoramaPage1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneDrinks;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneStarters;
        
        internal Microsoft.Phone.Controls.LongListSelector llStart;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneMains;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneDesert;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ClientApp;component/PanoramaPage1.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.panoPaneDrinks = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneDrinks")));
            this.panoPaneStarters = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneStarters")));
            this.llStart = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llStart")));
            this.panoPaneMains = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneMains")));
            this.panoPaneDesert = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneDesert")));
        }
    }
}
