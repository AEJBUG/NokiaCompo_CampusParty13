﻿#pragma checksum "C:\Users\ITR\Documents\NokiaCompo_CampusParty13-3fdb0de3d8ce05d3ed5be27410d562ffe4acc5d5\NokiaCompo_CampusParty13-3fdb0de3d8ce05d3ed5be27410d562ffe4acc5d5\Client\ClientApp\ClientApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "356684F7E689481DECCFB1B42AC6696B"
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneDrinks;
        
        internal Microsoft.Phone.Controls.LongListSelector llDrinks;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneStarters;
        
        internal Microsoft.Phone.Controls.LongListSelector llStarter;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneMains;
        
        internal Microsoft.Phone.Controls.LongListSelector llMain;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPaneDesert;
        
        internal Microsoft.Phone.Controls.LongListSelector llDesert;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoSummary;
        
        internal Microsoft.Phone.Controls.LongListSelector llSummary;
        
        internal Microsoft.Phone.Controls.PanoramaItem panoPay;
        
        internal System.Windows.Controls.TextBlock TotalCostBFTLabel;
        
        internal System.Windows.Controls.TextBlock ThankyouLable;
        
        internal System.Windows.Controls.TextBox toast;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/ClientApp;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.panoPaneDrinks = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneDrinks")));
            this.llDrinks = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llDrinks")));
            this.panoPaneStarters = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneStarters")));
            this.llStarter = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llStarter")));
            this.panoPaneMains = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneMains")));
            this.llMain = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llMain")));
            this.panoPaneDesert = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPaneDesert")));
            this.llDesert = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llDesert")));
            this.panoSummary = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoSummary")));
            this.llSummary = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("llSummary")));
            this.panoPay = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("panoPay")));
            this.TotalCostBFTLabel = ((System.Windows.Controls.TextBlock)(this.FindName("TotalCostBFTLabel")));
            this.ThankyouLable = ((System.Windows.Controls.TextBlock)(this.FindName("ThankyouLable")));
            this.toast = ((System.Windows.Controls.TextBox)(this.FindName("toast")));
        }
    }
}
