﻿#pragma checksum "..\..\..\..\Views\Pages\GameView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "077A256E41B1F82F5C2DABFD20B00F69F71A5FC5A222F424A619E7D6D568EBC6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Sudoku.UI.ValueConverters;
using Sudoku.UI.ViewModels;
using Sudoku.UI.Views.UserControls;
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


namespace Sudoku.UI.Views {
    
    
    /// <summary>
    /// GameView
    /// </summary>
    public partial class GameView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UndoButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NightModeButton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HintButton;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PlayArea;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TimeLabel;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel HistoryPanel;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView HistoryList;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Sudoku.UI.Views.UserControls.NumberSelector NumberSelector;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\..\Views\Pages\GameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton NoteToggle;
        
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
            System.Uri resourceLocater = new System.Uri("/Sudoku.UI;component/views/pages/gameview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Pages\GameView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.MenuButton = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.UndoButton = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.NightModeButton = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.HintButton = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.PlayArea = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.TimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.HistoryPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.HistoryList = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.NumberSelector = ((Sudoku.UI.Views.UserControls.NumberSelector)(target));
            return;
            case 11:
            this.NoteToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

