﻿#pragma checksum "..\..\..\Plist.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3DACA6ED4C6F77D70832EA52E6C1F12"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace wpf {
    
    
    /// <summary>
    /// Plist
    /// </summary>
    public partial class Plist : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel plistPanel;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label llistname;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox plist;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel controlPanel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bload;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button badd;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bremove;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox crepeat;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Plist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cshuffle;
        
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
            System.Uri resourceLocater = new System.Uri("/wpf;component/plist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Plist.xaml"
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
            
            #line 4 "..\..\..\Plist.xaml"
            ((wpf.Plist)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.plistPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.llistname = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.plist = ((System.Windows.Controls.ListBox)(target));
            
            #line 19 "..\..\..\Plist.xaml"
            this.plist.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.plist_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.controlPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.bload = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Plist.xaml"
            this.bload.Click += new System.Windows.RoutedEventHandler(this.bload_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.badd = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Plist.xaml"
            this.badd.Click += new System.Windows.RoutedEventHandler(this.badd_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bremove = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Plist.xaml"
            this.bremove.Click += new System.Windows.RoutedEventHandler(this.bremove_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.crepeat = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.cshuffle = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

