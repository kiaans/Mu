﻿

#pragma checksum "C:\Users\Sumeet\Mu\Mu_genotype1\Mu_genotype1\Trends.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B194B94FDF765B820FEE915B76955EA6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Mu_genotype1
{
    public partial class Trends : Mu_genotype1.Common.LayoutAwarePage
    {
        private Mu_genotype1.Common.LayoutAwarePage pageRoot;
        private Windows.UI.Xaml.Controls.ScrollViewer gridScrollViewer;
        private Windows.UI.Xaml.Controls.ScrollViewer snappedScrollViewer;
        private Windows.UI.Xaml.Controls.StackPanel gridLayoutPanel;
        private Windows.UI.Xaml.Controls.GridView itemsGridView;
        private Windows.UI.Xaml.Controls.TextBlock GNameTb;
        private Windows.UI.Xaml.Controls.Image GImage;
        private Windows.UI.Xaml.Controls.TextBlock GDesc;
        private Windows.UI.Xaml.Controls.Button backButton;
        private Windows.UI.Xaml.Controls.TextBlock pageTitle;
        private Windows.UI.Xaml.VisualState FullScreenLandscape;
        private Windows.UI.Xaml.VisualState Filled;
        private Windows.UI.Xaml.VisualState FullScreenPortrait;
        private Windows.UI.Xaml.VisualState Snapped;
        private bool _contentLoaded;

        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            Application.LoadComponent(this, new System.Uri("ms-appx:///Trends.xaml"), Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            pageRoot = (Mu_genotype1.Common.LayoutAwarePage)this.FindName("pageRoot");
            gridScrollViewer = (Windows.UI.Xaml.Controls.ScrollViewer)this.FindName("gridScrollViewer");
            snappedScrollViewer = (Windows.UI.Xaml.Controls.ScrollViewer)this.FindName("snappedScrollViewer");
            gridLayoutPanel = (Windows.UI.Xaml.Controls.StackPanel)this.FindName("gridLayoutPanel");
            itemsGridView = (Windows.UI.Xaml.Controls.GridView)this.FindName("itemsGridView");
            GNameTb = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("GNameTb");
            GImage = (Windows.UI.Xaml.Controls.Image)this.FindName("GImage");
            GDesc = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("GDesc");
            backButton = (Windows.UI.Xaml.Controls.Button)this.FindName("backButton");
            pageTitle = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("pageTitle");
            FullScreenLandscape = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenLandscape");
            Filled = (Windows.UI.Xaml.VisualState)this.FindName("Filled");
            FullScreenPortrait = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenPortrait");
            Snapped = (Windows.UI.Xaml.VisualState)this.FindName("Snapped");
        }
    }
}



