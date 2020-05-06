﻿#pragma checksum "..\..\RequirementsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3C8A1FD4329321ECF97EAADEE76ABCA01552B267973F687FBF1055A6F716CD16"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bakery_Order_Processing;
using Bakery_Order_Processing.Properties;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Bakery_Order_Processing {
    
    
    /// <summary>
    /// RequirementsWindow
    /// </summary>
    public partial class RequirementsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MI_Orders;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MI_Products;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MI_Requirements;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Cb_Language;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_Requirements;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Lbx_ProductionDates;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tbx_ProductionDateFilter;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Lbx_Requirements;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\RequirementsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Tblk_SelectedDate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bakery Order Processing;component/requirementswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RequirementsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\RequirementsWindow.xaml"
            ((Bakery_Order_Processing.RequirementsWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MI_Orders = ((System.Windows.Controls.MenuItem)(target));
            
            #line 26 "..\..\RequirementsWindow.xaml"
            this.MI_Orders.Click += new System.Windows.RoutedEventHandler(this.MI_Orders_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MI_Products = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\RequirementsWindow.xaml"
            this.MI_Products.Click += new System.Windows.RoutedEventHandler(this.MI_Products_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MI_Requirements = ((System.Windows.Controls.MenuItem)(target));
            
            #line 30 "..\..\RequirementsWindow.xaml"
            this.MI_Requirements.Click += new System.Windows.RoutedEventHandler(this.MI_Requirements_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Cb_Language = ((System.Windows.Controls.ComboBox)(target));
            
            #line 51 "..\..\RequirementsWindow.xaml"
            this.Cb_Language.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Cb_Language_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Grid_Requirements = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.Lbx_ProductionDates = ((System.Windows.Controls.ListBox)(target));
            
            #line 64 "..\..\RequirementsWindow.xaml"
            this.Lbx_ProductionDates.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Lbx_ProductionDates_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Tbx_ProductionDateFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 74 "..\..\RequirementsWindow.xaml"
            this.Tbx_ProductionDateFilter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Tbx_ProductionDateFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Lbx_Requirements = ((System.Windows.Controls.ListBox)(target));
            return;
            case 10:
            this.Tblk_SelectedDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
