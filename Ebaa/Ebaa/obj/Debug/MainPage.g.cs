﻿#pragma checksum "C:\Users\e3jvaisa\Desktop\Ebaa\Ebaa\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "51AC3B71923B051EC30F56BB3851F6EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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


namespace Ebaa {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton AppBarIconSettings;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivotMainPivot;
        
        internal System.Windows.Controls.TextBox txtBoxQuery;
        
        internal System.Windows.Controls.Button btnSearch;
        
        internal System.Windows.Controls.TextBox txtBoxMinPrice;
        
        internal System.Windows.Controls.TextBox txtBoxMaxPrice;
        
        internal System.Windows.Controls.TextBox txtBoxResults;
        
        internal Microsoft.Phone.Controls.ListPicker listPickerStore;
        
        internal Microsoft.Phone.Controls.ListPicker listPIckerSort;
        
        internal System.Windows.Controls.Button addPivotButton;
        
        internal System.Windows.Controls.ListBox lboxResult;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Ebaa;component/MainPage.xaml", System.UriKind.Relative));
            this.AppBarIconSettings = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("AppBarIconSettings")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivotMainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivotMainPivot")));
            this.txtBoxQuery = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxQuery")));
            this.btnSearch = ((System.Windows.Controls.Button)(this.FindName("btnSearch")));
            this.txtBoxMinPrice = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxMinPrice")));
            this.txtBoxMaxPrice = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxMaxPrice")));
            this.txtBoxResults = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxResults")));
            this.listPickerStore = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listPickerStore")));
            this.listPIckerSort = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listPIckerSort")));
            this.addPivotButton = ((System.Windows.Controls.Button)(this.FindName("addPivotButton")));
            this.lboxResult = ((System.Windows.Controls.ListBox)(this.FindName("lboxResult")));
        }
    }
}

