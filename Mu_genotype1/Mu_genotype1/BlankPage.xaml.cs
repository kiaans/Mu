﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.ApplicationSettings;

using Windows.System;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.Media;

using System.Net;
using System.Net.Http;
using System.Xml;
using System.Text;

using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mu_genotype1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            this.InitializeComponent();
            MediaControl.PlayPressed += MediaControl_PlayPressed;
            MediaControl.PausePressed += MediaControl_PausePressed;
            MediaControl.PlayPauseTogglePressed += MediaControl_PlayPauseTogglePressed;
            MediaControl.StopPressed += MediaControl_StopPressed;
 
        }

        private void MediaControl_StopPressed(object sender, object e)
        {
            mediaPlayer.Stop();
            MediaControl.IsPlaying = false;
        }

        private void MediaControl_PlayPauseTogglePressed(object sender, object e)
        {/*
            if (MediaControl.IsPlaying == true)
            {
                mediaPlayer.Pause();
                MediaControl.IsPlaying = false;
            }
            else
            {
                mediaPlayer.Play();
                MediaControl.IsPlaying = true;
            }*/
        }

        private void MediaControl_PausePressed(object sender, object e)
        {
            mediaPlayer.Pause();
        }

        private void MediaControl_PlayPressed(object sender, object e)
        {
            mediaPlayer.Play();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        string lfm_api_key = Globalv.lfm_api_key;
        MusicProperties id3;

        private void mediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            SongTitle.Text = "Play a song now!";
            Artist.Text = "Swipe up from bottom to view your Music collection.";
        }
        
        private async void Collection_Click_1(object sender, RoutedEventArgs e)
        {
            if (EnsureUnsnapped())
            {
                FileOpenPicker pkr = new FileOpenPicker();
                pkr.ViewMode = PickerViewMode.List;
                pkr.SuggestedStartLocation = PickerLocationId.MusicLibrary;
                pkr.FileTypeFilter.Add(".mp3");

                StorageFile file = await pkr.PickSingleFileAsync();
                
                if (null != file)
                {
                    var strm = await file.OpenAsync(FileAccessMode.Read);
                    Playlist.NowPlaying.Clear();
                    mediaPlayer.AudioCategory = Windows.UI.Xaml.Media.AudioCategory.BackgroundCapableMedia;
                    mediaPlayer.SetSource(strm, file.ContentType);
                    MediaControl.IsPlaying = true;
                    id3 = await file.Properties.GetMusicPropertiesAsync();
                    SongTitle.Text = id3.Title;
                    Artist.Text = id3.Artist;                    

                    Playlist.NowPlaying.Add(id3);
                    Lastfm.track_updateNowPlaying(id3);

                    string xmlinfo = await Lastfm.track_getInfo(id3);
                    try
                    {
                        DebugTB.Text = xmlinfo;

                        using (XmlReader rd = XmlReader.Create(new StringReader(xmlinfo)))
                        {
                            rd.ReadToFollowing("name");
                            TitleInfoTbx.Text = rd.ReadElementContentAsString();
                            rd.ReadToFollowing("artist");
                            rd.ReadToDescendant("name");
                            SubtitleInfoTbx.Text = rd.ReadElementContentAsString();                            
                        }
                        
                        using(XmlReader rd=XmlReader.Create(new StringReader(xmlinfo)))
                        {
                            rd.ReadToFollowing("image");
                            rd.ReadToNextSibling("image");
                            rd.ReadToNextSibling("image");
                            Uri src = new Uri(rd.ReadElementContentAsString(), UriKind.Absolute);
                            AlbumArtHolder.Source = new BitmapImage(src);                            
                        }
                        using (XmlReader rd = XmlReader.Create(new StringReader(xmlinfo)))
                        {
                            rd.ReadToFollowing("wiki");
                            rd.ReadToDescendant("summary");
                            SummaryInfoTbx.Text = rd.ReadElementContentAsString();
                        }
                    }
                    catch (Exception) { AlbumArtHolder.Source = null; }

                    //prepare for scrobble
                    TimelineMarker tlm = new TimelineMarker();
                    tlm.Time = new System.TimeSpan(0,0,(int)id3.Duration.TotalSeconds/2);
                    mediaPlayer.Markers.Add(tlm);
                    if (id3.Duration > new System.TimeSpan(0, 0, 30))
                    {
                        mediaPlayer.MarkerReached += mediaPlayer_MarkerReached_scrobble; //scrobble
                    }
                }
                else
                { return; }
            }
        }

        async void mediaPlayer_MarkerReached_scrobble(object sender, TimelineMarkerRoutedEventArgs e)
        {
            DebugTB.Text = await Lastfm.track_scrobble(id3);
            //throw new NotImplementedException();
        }

        bool EnsureUnsnapped()
        {
            return ((ApplicationView.Value != ApplicationViewState.Snapped) ||
                ApplicationView.TryUnsnap());
        }


        private async void LoginBtn_Click_1(object sender, RoutedEventArgs e)
        {
            HttpClient cli = new HttpClient();

            string username = UsernameTBx.Text;

            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm("MD5");
            CryptographicHash objHash = objAlgProv.CreateHash();

            //md5(pw)
            IBuffer buffpw = CryptographicBuffer.ConvertStringToBinary(PwBx.Password, BinaryStringEncoding.Utf8);
            objHash.Append(buffpw);
            IBuffer buffpwHash = objHash.GetValueAndReset();
            string pwHash = CryptographicBuffer.EncodeToHexString(buffpwHash);

            //authtoken=md5(username+md5(pw))
            IBuffer bufftok = CryptographicBuffer.ConvertStringToBinary(String.Concat(UsernameTBx.Text, pwHash), BinaryStringEncoding.Utf8);
            objHash.Append(bufftok);
            IBuffer bufftokHash = objHash.GetValueAndReset();
            string lfm_authToken = CryptographicBuffer.EncodeToHexString(bufftokHash);

            //signature
            string apiSig = "api_key" + lfm_api_key + "authToken" + lfm_authToken + "methodauth.getMobileSessionusername" + UsernameTBx.Text + "0e6e780c3cfa3faedf0c58d5aa6de92f";
            IBuffer buffSig = CryptographicBuffer.ConvertStringToBinary(apiSig, BinaryStringEncoding.Utf8);
            objHash.Append(buffSig);
            IBuffer buffSighash = objHash.GetValueAndReset();
            string lfm_api_sig = CryptographicBuffer.EncodeToHexString(buffSighash);

            string auth_request = "http://ws.audioscrobbler.com/2.0/?method=auth.getMobileSession&username=" + UsernameTBx.Text + "&authToken=" + lfm_authToken + "&api_key=" + lfm_api_key + "&api_sig=" + lfm_api_sig;
            HttpResponseMessage auth = await cli.GetAsync(auth_request);
            //auth.EnsureSuccessStatusCode();
            string auth_response_content = await auth.Content.ReadAsStringAsync();

            using (XmlReader rd = XmlReader.Create(new StringReader(auth_response_content)))
            {
                rd.ReadToFollowing("key");
                Globalv.session_key = rd.ReadElementContentAsString();
            }
            DebugTB.Text = Globalv.session_key;

        }

        private void SigninButton_Click_1(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Visible;
            LoginGridAppear.Begin();
        }

        private void MainPanel_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            if(LoginGrid.Visibility == Visibility.Visible)
                LoginGridDisappear.Begin();
            LoginGridDisappear.Completed += (o,dummy) => { LoginGrid.Visibility = Visibility.Collapsed; };
        }

        private void InfoBtn_Click_1(object sender, RoutedEventArgs e)
        {
            InfoGrid.Visibility = Visibility.Visible;
            InfoAppear.Begin();
        }

        private void AppbarClosed(object sender, object e)
        {
            if(InfoGrid.Visibility == Visibility.Visible)
                InfoDisappear.Begin();
            InfoDisappear.Completed += (o, dummy) => { InfoGrid.Visibility = Visibility.Collapsed; };
        }

        private void RecoArtistsButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecoArtists));
        }

        private void RecoTracksButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecoTracks));
        }

        private void TrendsButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Trends));
        }


        private void TweetButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Tweet));
        }

        private void MapButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapMusic));
        }

    }
}
