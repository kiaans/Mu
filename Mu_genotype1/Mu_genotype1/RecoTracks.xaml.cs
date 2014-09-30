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
using Windows.Storage.FileProperties;

using System.Xml;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Mu_genotype1
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class RecoTracks : Mu_genotype1.Common.LayoutAwarePage
    {
        public RecoTracks()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the collection of items to be displayed.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.DefaultViewModel["Items"] = e.Parameter;
            Song s = new Song();
            s.Artist = Playlist.NowPlaying[0].Artist;
            s.Title = Playlist.NowPlaying[0].Title;
            string resp = await Lastfm.track_getSimilar(s);

            Globalv.RecommendedTracks.Clear();

            using (XmlReader rd = XmlReader.Create(new StringReader(resp)))
            {
                try
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Song p = new Song();
                        rd.ReadToFollowing("track");
                        rd.ReadToDescendant("name");
                        p.Title = rd.ReadElementContentAsString();
                        rd.ReadToNextSibling("artist");
                        rd.ReadToDescendant("name");
                        p.Artist = rd.ReadElementContentAsString();
                        rd.ReadToFollowing("image");
                        rd.ReadToNextSibling("image");
                        rd.ReadToNextSibling("image");
                        p.image = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(rd.ReadElementContentAsString(), UriKind.Absolute));

                        Globalv.RecommendedTracks.Add(p);
                    }
                }
                catch (Exception) { }
                itemGridView.ItemsSource = Globalv.RecommendedTracks;

            }                       

        }

        private void itemGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(TrackDetails), e.ClickedItem);

        }
    }
}
