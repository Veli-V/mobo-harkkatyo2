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
        public void doSavedSearches(){
            foreach (Search sh in App.savedSearches){
                doSearch(sh);
            }
        }


 
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            List<String> stores = new List<string>();
            stores.Add("USA");
            stores.Add("United Kingdom");
            stores.Add("Germany");
            stores.Add("Hong Kong");
            listPickerStore.ItemsSource = stores;
            List<String> sorts = new List<String>();
            sorts.Add("Best match");
            sorts.Add("Newest");
            sorts.Add("End time soon");
            sorts.Add("Price highest");
            sorts.Add("Price lowest");
            listPIckerSort.ItemsSource = sorts;


            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            txtBoxQuery.Text = App.defaultSearch;
            doSavedSearches();

        }

        public static string returnSort(string s)
        {
            if ( s == "Best match")
                return "BestMatch";
            if ( s == "End time soon")
                return "EndTimeSoonest";
            if ( s == "Newest")
                return "StartTimeNewest";
            if ( s == "Price highest")
                return "PricePlusShippingHighest";
            if ( s == "Price lowest")
                return "PricePlusShippingLowest";
            return "BestMatch";

        }

        public static string returnStore(String s){
            if (s == "USA")
                return "EBAY-US";
            if (s == "United Kingdom")
                return "EBAY-GB";
            if (s == "Germany")
                return "EBAY-DE";
            if (s == "Honk Kong")
                return "EBAY-HK";
            else
                return "EBAY-GB";
        }

        protected virtual void OnNavigatedTo()
        {
            doSavedSearches();
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



            String urlString = returnUrl();

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
                if (res.searchResult == null)
                {
                    MessageBox.Show("ei löytynyt");
                    break;
                }
                foreach (SearchResult searchResult in res.searchResult)
                {
                    if (searchResult.item == null)
                    {
                        MessageBox.Show("ei löytynyt");
                        break;
                    }
                    foreach (Item item in searchResult.item)
                    {
                        lboxResult.Items.Add(item);
                    }
                }
            }
        }

        private string returnUrl()
        {
            String urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0";
            urlString += "&GLOBAL-ID=" + returnStore(listPickerStore.SelectedItem.ToString());
            urlString += "&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1";
            urlString += "&keywords=" + txtBoxQuery.Text;
            urlString += "&paginationInput.entriesPerPage=" + txtBoxResults.Text;
            urlString += "&sortOrder=" + returnSort(listPIckerSort.SelectedItem.ToString());
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

            return urlString;
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


        private void addPivotItem(object sender, System.Windows.RoutedEventArgs e)
        {
            String q = txtBoxQuery.Text;
            String mip = txtBoxMinPrice.Text;
            String map = txtBoxMaxPrice.Text;
            String rc = txtBoxResults.Text;
            String s = returnStore(listPickerStore.SelectedItem.ToString());
            String st = returnSort(listPIckerSort.SelectedItem.ToString());
            
            TextBox txtBox = new TextBox();
            txtBox.Text = q;
            CustomMessageBox mb = new CustomMessageBox()
            {
                Caption = "You're saving a search",
                Message = "Enter name for Search",
                LeftButtonContent = "Save",
                RightButtonContent = "Cancel",
                Content = txtBox
            };
            mb.Dismissed += (ss, boxEventArgs) =>
                {
                    switch (boxEventArgs.Result)
                    {
                        case CustomMessageBoxResult.LeftButton:
                            string n = txtBox.Text;
                            Search search = new Search(q, mip, map, rc, s, n, st);
                            App.savedSearches.Add(search);
                            doSearch(search);
                            break;
                        case CustomMessageBoxResult.RightButton:
                            // Do something.
                            break;
                        case CustomMessageBoxResult.None:
                            // Do something.
                            break;
                        default:
                            break;
                    }
                };
            mb.Show();
        }

        private void doSearch(Search sh)
        {
            String urlString = sh.createUrl();
            Uri url = new Uri(urlString);

            WebClient webClient = new WebClient();

            ListBox lb = new ListBox();
            lb.ItemTemplate = (DataTemplate)this.Resources["DataTemplate1"];

            MessageBox.Show(urlString);
            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted+=new DownloadStringCompletedEventHandler((sender, e) => this.webClient_DownloadStringCompleted_2(sender, e, lb));

            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler((sender, e) => this.webClient_DownloadStringCompleted(sender, e, localItem.MyListBox));
            //private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e,ListBox listBox)
            PivotItem pivotti = new PivotItem();
            pivotti.Content = lb;
            pivotti.Header = sh.name_;
            pivotMainPivot.Items.Add(pivotti);


        }

        void webClient_DownloadStringCompleted_2(object sender, DownloadStringCompletedEventArgs e, ListBox lb)
        {

            // Siivotaan json vastauksesta @ merkit pois. Ebayn api on nimettyn jotkin
            // muuttujat siten että alkavat @-merkillä. Tämä ei kuitankaan oikein
            // toimi tämän ohjelman kannalta, joten siivotaan ne pois. Tämän
            // jälkeen parsiminen onnnistuu.
            string pattern = "@";
            Regex rgx = new Regex(pattern);
            string tmp = rgx.Replace(e.Result.ToString(), "");

            // parsitaan JSON RootObjecteiksi
            RootObject dataa = (RootObject)JsonConvert.DeserializeObject<RootObject>(tmp);

            // koska JSON palauttaa listoja, niin käydään listojen kaikki alkiot läpi
            // näin monta sisäkkäistä, koska tulos ketjuttuu mukavasti.
            // item on se mitä me halutaan tarkemmin tutkia, eli yksittäinen myynnissä oleva
            // esine.
            foreach (FindItemsAdvancedResponse res in dataa.findItemsAdvancedResponse)
            {
                if (res.searchResult == null)
                {
                    MessageBox.Show("ei löytynyt");
                    break;
                }
                foreach (SearchResult searchResult in res.searchResult)
                {
                    if (searchResult.item == null)
                    {
                        MessageBox.Show("ei löytynyt");
                        break;
                    }
                    foreach (Item item in searchResult.item)
                    {
                        lb.Items.Add(item);
                    }
                }
            }
        }

        private void AppBarIconSettingsClicked(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

       

    }
}
