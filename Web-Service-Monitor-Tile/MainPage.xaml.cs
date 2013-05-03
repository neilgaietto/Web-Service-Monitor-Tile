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
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using NotificationsExtensions.TileContent;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Web_Service_Monitor_Tile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            txtConnection.Text = "http://www.neilgaietto.com/API/Stats?json=true";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //http://msdn.microsoft.com/en-us/library/windows/apps/xaml/Hh868253%28v=win.10%29.aspx
            //http://blogs.msdn.com/b/b8/archive/2011/11/02/updating-live-tiles-without-draining-your-battery.aspx

            ////square
            //var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText01);

            //var tileTextAttributes = tileXml.GetElementsByTagName("text");
            //tileTextAttributes[0].InnerText = "Web service";


            ////rec
            //var recTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText01);
            //var recTextAttributes = recTemplate.GetElementsByTagName("text");
            //recTextAttributes[0].InnerText = "Web service";

            ////append rec to square
            //IXmlNode node = tileXml.ImportNode(recTemplate.GetElementsByTagName("binding").Item(0), true);
            //tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);


            ////send tile notification
            //TileNotification tileNotification = new TileNotification(tileXml);
            ////tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(10);

            //TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);


            // create the wide template
            ITileWideText01 tileContent = TileContentFactory.CreateTileWideText01();
            tileContent.TextHeading.Text = "Web service call";
            tileContent.TextBody1.Text = "a";
            tileContent.TextBody2.Text = "blah";
            tileContent.TextBody3.Text = "c";
            tileContent.TextBody4.Text = "d";
            
            // Users can resize tiles to square or wide.
            // Apps can choose to include only square assets (meaning the app's tile can never be wide), or
            // include both wide and square assets (the user can resize the tile to square or wide).
            // Apps cannot include only wide assets.

            // Apps that support being wide should include square tile notifications since users
            // determine the size of the tile.

            // create the square template and attach it to the wide template
            ITileSquareText04 squareContent = TileContentFactory.CreateTileSquareText04();
            squareContent.TextBodyWrap.Text = "Web service call";
            tileContent.SquareContent = squareContent;


            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            // send the notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());

            txtTestResults.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtTestResults.Text = tileContent.GetContent();
        }

        async private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            progTest.IsActive = true;
            txtTestResults.Text = "";
            //try
            //{
            JsonObject results = await Logic.Webservices.Get(txtConnection.Text);
            txtTestResults.Visibility = Windows.UI.Xaml.Visibility.Visible;

            foreach (var r in results)
            {
                txtTestResults.Text += r.Key + ": " + r.Value.Stringify() + Environment.NewLine;
            }
            //}
            //catch (Exception ex)
            //{
            //    txtTestResults.Text = ex.Message;
            //}
            progTest.IsActive = false;
        }
    }
}
