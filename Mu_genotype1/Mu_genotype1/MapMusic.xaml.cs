using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml;
using Bing.Maps;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Mu_genotype1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MapMusic : Mu_genotype1.Common.LayoutAwarePage
    {
        public MapMusic()
        {
            this.InitializeComponent();
            myMap.Center = new Location(21.7679, 78.8718);
            myMap.ZoomLevel = 4;
            myMap.MapType = MapType.Birdseye;
        }

        private async void myMap_Loaded_1(object sender, RoutedEventArgs e)
        {
            //TODO: save response once in a file and avoid further http requests
            string resp = await Lastfm.geo_Metros();
            using (XmlReader rd = XmlReader.Create(new StringReader(resp)))
            {
                try
                {
                    while (true)
                    {
                        Metropolis m = new Metropolis();
                        rd.ReadToFollowing("metro");
                        rd.ReadToDescendant("name");
                        m.name = rd.ReadElementContentAsString();
                        rd.ReadToNextSibling("country");
                        m.country = rd.ReadElementContentAsString();
                        Globalv.AllMetros.Add(m);
                    }
                }
                catch(Exception) { }
            }
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

    }
}
