﻿#pragma checksum "..\..\PingIndicatorWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C8649E288C4D257A82DFA1FAF3CAAFE81AB85A81"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PingIndicator;
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


namespace PingIndicator {
    
    
    /// <summary>
    /// PingIndicatorWindow
    /// </summary>
    public partial class PingIndicatorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PingIndicator.PingIndicatorWindow _this;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PingIntervalBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PingNumberBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer MainScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\PingIndicatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock OutputBox;
        
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
            System.Uri resourceLocater = new System.Uri("/PingIndicator;component/pingindicatorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PingIndicatorWindow.xaml"
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
            this._this = ((PingIndicator.PingIndicatorWindow)(target));
            return;
            case 2:
            this.AddressBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.PingIntervalBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 35 "..\..\PingIndicatorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PingOnce_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 36 "..\..\PingIndicatorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PingXTimes_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PingNumberBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.MainScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 54 "..\..\PingIndicatorWindow.xaml"
            this.MainScrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(this.MainScrollViewer_ScrollChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.OutputBox = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

