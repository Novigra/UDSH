﻿#pragma checksum "..\..\..\..\View\NewFileCreationWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7B6173006E7287284D44164A75967B10CA1224DF"
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
using UDSH.MVVM;
using UDSH.View;


namespace UDSH.View {
    
    
    /// <summary>
    /// NewFileCreationWindow
    /// </summary>
    public partial class NewFileCreationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WindowLayout;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HighlightText;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserSearchBox;
        
        #line default
        #line hidden
        
        
        #line 226 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FileWarningMessage;
        
        #line default
        #line hidden
        
        
        #line 669 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FolderHighlight;
        
        #line default
        #line hidden
        
        
        #line 698 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup DirectoryPopup;
        
        #line default
        #line hidden
        
        
        #line 701 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform DirectoryBorderTranslate;
        
        #line default
        #line hidden
        
        
        #line 810 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup DirectoryMessageWarning;
        
        #line default
        #line hidden
        
        
        #line 813 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WarningMessage;
        
        #line default
        #line hidden
        
        
        #line 934 "..\..\..\..\View\NewFileCreationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UDSH.MVVM.CustomScrollViewer DirectoryNameScrollViewer;
        
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
            System.Uri resourceLocater = new System.Uri("/UDSH;V0.1.0.0;component/view/newfilecreationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\NewFileCreationWindow.xaml"
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
            
            #line 31 "..\..\..\..\View\NewFileCreationWindow.xaml"
            ((System.Windows.Media.Animation.DoubleAnimation)(target)).Completed += new System.EventHandler(this.Storyboard_Completed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.WindowLayout = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.HighlightText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.UserSearchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.FileWarningMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.FolderHighlight = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.DirectoryPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 8:
            this.DirectoryBorderTranslate = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 9:
            
            #line 749 "..\..\..\..\View\NewFileCreationWindow.xaml"
            ((System.Windows.Controls.Button)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SetModalWindowActivation);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 794 "..\..\..\..\View\NewFileCreationWindow.xaml"
            ((System.Windows.Controls.ListView)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SetModalWindowActivation);
            
            #line default
            #line hidden
            return;
            case 11:
            this.DirectoryMessageWarning = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 12:
            this.WarningMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.DirectoryNameScrollViewer = ((UDSH.MVVM.CustomScrollViewer)(target));
            return;
            case 14:
            
            #line 953 "..\..\..\..\View\NewFileCreationWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SetModalWindowActivation);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 975 "..\..\..\..\View\NewFileCreationWindow.xaml"
            ((System.Windows.Controls.Button)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SetModalWindowActivation);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

