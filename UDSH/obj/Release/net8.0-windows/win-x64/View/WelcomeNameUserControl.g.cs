﻿#pragma checksum "..\..\..\..\..\View\WelcomeNameUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0648777CF0D2F2DFB646252D7B7F83B54F2EC081"
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
    /// WelcomeNameUserControl
    /// </summary>
    public partial class WelcomeNameUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border NameBorder;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HighlightText;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameText;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UpperPara;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Para;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BottomPara;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ButtonContainer;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextButton;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
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
            System.Uri resourceLocater = new System.Uri("/UDSH;V0.1.0.0;component/view/welcomenameusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
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
            
            #line 33 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.HighlightText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.NameText = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
            this.NameText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameText_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UpperPara = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Para = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.BottomPara = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.ButtonContainer = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.NextButton = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\..\..\View\WelcomeNameUserControl.xaml"
            this.NextButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 10:
            this.NextImg = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

