﻿

#pragma checksum "C:\Users\Sumeet\Mu\Mu_genotype1\Mu_genotype1\RecoTracks.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A1224B5A3BBD48F82D951510058BD0E"
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
    public partial class RecoTracks : Mu_genotype1.Common.LayoutAwarePage, IComponentConnector
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 52 "..\..\..\RecoTracks.xaml"
                ((Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 35 "..\..\..\RecoTracks.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


