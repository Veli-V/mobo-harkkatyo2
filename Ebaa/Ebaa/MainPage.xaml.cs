using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.Phone.Tasks;


namespace Ebaa
{
    public partial class MainPage : PhoneApplicationPage
    {
        
 
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            List<String> stores = new List<string>();
            stores.Add("US");
            stores.Add("UK");
            stores.Add("DE");
            listPickerStore.ItemsSource = stores;

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            txtBoxQuery.Text = App.defaultSearch;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void btnSearchClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            // GET call = http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0&GLOBAL-ID=EBAY-GB&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1&keywords=Dark+elf&paginationInput.entriesPerPage=10&sortOrder=StartTimeNewest&itemFilter(0).name=ListingType&itemFilter(0).value=FixedPrice&itemFilter(1).name=MinPrice&itemFilter(1).value=0.00&itemFilter(2).name=MaxPrice&itemFilter(2).value=25.00&affiliate.networkId=9&affiliate.trackingId=1234567890&affiliate.customId=456&RESPONSE-DATA-FORMAT=XML 


            String urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0&GLOBAL-ID=EBAY-GB&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1";
            urlString += "&keywords=" + txtBoxQuery.Text;
            urlString += "&paginationInput.entriesPerPage=" + txtBoxResults.Text;
            urlString += "&sortOrder=StartTimeNewest";
            urlString += "&itemFilter(0).name=ListingType";
            urlString += "&itemFilter(0).value=FixedPrice";
            urlString += "&itemFilter(1).name=MinPrice";
            urlString += "&itemFilter(1).value=" + txtBoxMinPrice.Text;
            urlString += "&itemFilter(2).name=MaxPrice";
            urlString += "&itemFilter(2).value=" + txtBoxMaxPrice.Text;
            urlString += "&affiliate.networkId=9";
            urlString += "&affiliate.trackingId=1234567890";
            urlString += "&affiliate.customId=456";
            urlString += "&RESPONSE-DATA-FORMAT=JSON";


            Uri url = new Uri(urlString);

            WebClient webClient = new WebClient();

            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            lboxResult.Items.Clear();

            // Siivotaan json vastauksesta @ merkit pois. Ebayn api on nimettyn jotkin
            // muuttujat siten että alkavat @-merkillä. Tämä ei kuitankaan oikein
            // toimi tämän ohjelman kannalta, joten siivotaan ne pois. Tämän
            // jälkeen parsiminen onnnistuu.
            string pattern = "@";
            Regex rgx = new Regex(pattern);
            string tmp = rgx.Replace(e.Result.ToString(), "");
            
            // parsitaan JSON RootObjecteiksi
            RootObject dataa = (RootObject)JsonConvert.DeserializeObject<RootObject>(tmp);

            //List<Item> itemList = new List<Item>();

            // koska JSON palauttaa listoja, niin käydään listojen kaikki alkiot läpi
            // näin monta sisäkkäistä, koska tulos ketjuttuu mukavasti.
            // item on se mitä me halutaan tarkemmin tutkia, eli yksittäinen myynnissä oleva
            // esine.
            foreach (FindItemsAdvancedResponse res in dataa.findItemsAdvancedResponse)
            {
                foreach (SearchResult searchResult in res.searchResult)
                {
                    foreach (Item item in searchResult.item)
                    {
                        lboxResult.Items.Add(item);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tmpButton = (Button)sender;
            string url = tmpButton.CommandParameter.ToString();
            //MessageBox.Show(url);
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);
            webBrowserTask.Show();
        }

        // Haetaan uuteen pivot itemiin tuloslista
        private List<Item> getResultList()
        {

            string urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0&GLOBAL-ID=EBAY-GB&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1&keywords=Dark+elf&paginationInput.entriesPerPage=10&sortOrder=StartTimeNewest&itemFilter(0).name=ListingType&itemFilter(0).value=FixedPrice&itemFilter(1).name=MinPrice&itemFilter(1).value=0.00&itemFilter(2).name=MaxPrice&itemFilter(2).value=25.00&affiliate.networkId=9&affiliate.trackingId=1234567890&affiliate.customId=456&RESPONSE-DATA-FORMAT=XML";
            Uri url = new Uri(urlString);

            List<Item> tmpList = new List<Item>();
            WebClient webClient = new WebClient();

            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted_2);
            return tmpList;
        }

        void webClient_DownloadStringCompleted_2(object sender, DownloadStringCompletedEventArgs e)
        {
            

            // Siivotaan json vastauksesta @ merkit pois. Ebayn api on nimettyn jotkin
            // muuttujat siten että alkavat @-merkillä. Tämä ei kuitankaan oikein
            // toimi tämän ohjelman kannalta, joten siivotaan ne pois. Tämän
            // jälkeen parsiminen onnnistuu.
            string pattern = "@";
            Regex rgx = new Regex(pattern);
            string tmp = rgx.Replace(e.Result.ToString(), "");

            // parsitaan JSON RootObjecteiksi
            //RootObject dataa = (RootObject)JsonConvert.DeserializeObject<RootObject>(tmp);


        }

        private void addPivotItem(object sender, System.Windows.RoutedEventArgs e)
        {
            /*
        	// TODO: Add event handler implementation here.
            PivotItem pivotti = new PivotItem();
            pivotti.Header = "Uusi";
            ListBox lbox = new ListBox();
            lbox.ItemTemplate = (DataTemplate)this.Resources["DataTemplate1"];
            lbox.ItemsSource = getResultList();
            pivotti.Content = lbox;
            pivotMainPivot.Items.Add(pivotti);
            */

            Search search = new Search();
            (search.pivotItem_.Content as ListBox).ItemTemplate = (DataTemplate)this.Resources["DataTemplate1"];
            pivotMainPivot.Items.Add(search.pivotItem_);
            App.savedSearches.Add(search);
            search.DoSearch();
            MessageBox.Show(App.savedSearches.ToString());
 
        }

        private void AppBarIconSettingsClicked(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

       

    }
}
