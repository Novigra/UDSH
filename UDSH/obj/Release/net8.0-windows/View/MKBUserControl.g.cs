﻿#pragma checksum "..\..\..\..\View\MKBUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EA20CEBC9410E2DC31530B3DE14CB21B3083D36B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
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
using UDSH.Converters;
using UDSH.View;
using UDSH.ViewModel;


namespace UDSH.View {
    
    
    /// <summary>
    /// MKBUserControl
    /// </summary>
    public partial class MKBUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ParentGrid;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NoteButton;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MainCanvas;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UDSH.View.EllipseCanvas ECanvas;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderCollision;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LeftCollision;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border RightCollision;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BottomCollision;
        
        #line default
        #line hidden
        
        
        #line 250 "..\..\..\..\View\MKBUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SideContentGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/UDSH;V0.1.0.0;component/view/mkbusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\MKBUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.ParentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.NoteButton = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.MainCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 113 "..\..\..\..\View\MKBUserControl.xaml"
            this.MainCanvas.ContextMenuOpening += new System.Windows.Controls.ContextMenuEventHandler(this.MainCanvas_ContextMenuOpening);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ECanvas = ((UDSH.View.EllipseCanvas)(target));
            return;
            case 5:
            this.BorderCollision = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.LeftCollision = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.RightCollision = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.BottomCollision = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.SideContentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

