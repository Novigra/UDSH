﻿#pragma checksum "..\..\..\..\..\View\CSFUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9F928098A67B84301657837C6E477B719750FF74"
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
    /// CSFUserControl
    /// </summary>
    public partial class CSFUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TestActive;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TestCombo;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TestCapture;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TestGrid;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TesstList;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander TestExpander;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\..\..\View\CSFUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle TestContent;
        
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
            System.Uri resourceLocater = new System.Uri("/UDSH;V0.1.0.0;component/view/csfusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\CSFUserControl.xaml"
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
            this.TestActive = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.TestCombo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TestCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TestCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TestCapture = ((System.Windows.Controls.Border)(target));
            
            #line 98 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TestCapture.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 99 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TestCapture.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 100 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TestCapture.MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TestCapture.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TestCapture_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TestGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.TesstList = ((System.Windows.Controls.ListView)(target));
            
            #line 135 "..\..\..\..\..\View\CSFUserControl.xaml"
            this.TesstList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TesstList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TestExpander = ((System.Windows.Controls.Expander)(target));
            return;
            case 7:
            this.TestContent = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

