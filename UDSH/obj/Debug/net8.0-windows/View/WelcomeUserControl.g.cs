﻿#pragma checksum "..\..\..\..\View\WelcomeUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D33E5269332F5CCF349B7BDEDDEA292859504EF2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using System.Windows.Controls.Ribbon;
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
using UDSH.View;


namespace UDSH.View {
    
    
    /// <summary>
    /// WelcomeUserControl
    /// </summary>
    public partial class WelcomeUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 78 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WelcomeTitle;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WelcomeSubTitle;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UpperPara;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Para;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BottomPara;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ButtonContainer;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\..\View\WelcomeUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NextImg;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UDSH;V0.1.0.0;component/view/welcomeusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\WelcomeUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 52 "..\..\..\..\View\WelcomeUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.WelcomeTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.WelcomeSubTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.UpperPara = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Para = ((System.Windows.Controls.TextBlock)(target));
            
            #line 144 "..\..\..\..\View\WelcomeUserControl.xaml"
            this.Para.Loaded += new System.Windows.RoutedEventHandler(this.TextBlock_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BottomPara = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.ButtonContainer = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            
            #line 170 "..\..\..\..\View\WelcomeUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.NextImg = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

