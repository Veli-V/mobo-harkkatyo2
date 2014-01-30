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

namespace Ebaa
{
    public partial class MainPage : PhoneApplicationPage
    {

 
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
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
            
            /*
             *         	// luodaan webclient ja tarvittava urli
            WebClient webClient = new WebClient();
            string url = urlBase + "lat=" + latitude + "&lng=" + longitude + "&username=" + username;
            Uri uri = new Uri(url);

            // varsinainen kutsu
            webClient.DownloadStringAsync(uri);
            // Määritetään async lataukselle
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
             */

            String urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0&GLOBAL-ID=EBAY-GB&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1&keywords=Dark+elf&paginationInput.entriesPerPage=10&sortOrder=StartTimeNewest&itemFilter(0).name=ListingType&itemFilter(0).value=FixedPrice&itemFilter(1).name=MinPrice&itemFilter(1).value=0.00&itemFilter(2).name=MaxPrice&itemFilter(2).value=25.00&affiliate.networkId=9&affiliate.trackingId=1234567890&affiliate.customId=456&RESPONSE-DATA-FORMAT=JSON";
            Uri url = new Uri(urlString);

            WebClient webClient = new WebClient();

            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            
            RootObject dataa = (RootObject)JsonConvert.DeserializeObject<RootObject>(e.Result);
            MessageBox.Show(dataa.findItemsAdvancedResponse.ToString());
            string tmp = dataa.findItemsAdvancedResponse[0].searchResult[0].item[0].title[0];
            txtblockTulos.Text = tmp;
            List<Item> itemList = new List<Item>();
            foreach (FindItemsAdvancedResponse res in dataa.findItemsAdvancedResponse)
            {
                foreach (SearchResult searchResult in res.searchResult)
                {
                    foreach (Item item in searchResult.item)
                    {
                        lboxResult.Items.Add(item);
                        //MessageBox.Show(item.title[0]);
                    }
                }
            }
            //lboxResult.ItemsSource = itemList;
        }
    }
    public class PrimaryCategory
{
    public List<string> categoryId { get; set; }
    public List<string> categoryName { get; set; }
}

public class ProductId
{
    public string invalid_name_type { get; set; }
    public string value { get; set; }
}

public class ShippingServiceCost
{
    public string invalid_name_currencyId { get; set; }
    public string value { get; set; }
}

public class ShippingInfo
{
    public List<ShippingServiceCost> shippingServiceCost { get; set; }
    public List<string> shippingType { get; set; }
    public List<string> shipToLocations { get; set; }
}

public class CurrentPrice
{
    public string invalid_name_currencyId { get; set; }
    public string value { get; set; }
}

public class ConvertedCurrentPrice
{
    public string invalid_name_currencyId { get; set; }
    public string value { get; set; }
}

public class SellingStatu
{
    public List<CurrentPrice> currentPrice { get; set; }
    public List<ConvertedCurrentPrice> convertedCurrentPrice { get; set; }
    public List<string> sellingState { get; set; }
    public List<string> timeLeft { get; set; }
}

public class ListingInfo
{
    public List<string> bestOfferEnabled { get; set; }
    public List<string> buyItNowAvailable { get; set; }
    public List<string> startTime { get; set; }
    public List<string> endTime { get; set; }
    public List<string> listingType { get; set; }
    public List<string> gift { get; set; }
}

public class Condition
{
    public List<string> conditionId { get; set; }
    public List<string> conditionDisplayName { get; set; }
}

public class Item
{
    public List<string> itemId { get; set; }
    public List<string> title { get; set; }
    public List<string> globalId { get; set; }
    public List<PrimaryCategory> primaryCategory { get; set; }
    public List<string> galleryURL { get; set; }
    public List<string> viewItemURL { get; set; }
    public List<ProductId> productId { get; set; }
    public List<string> paymentMethod { get; set; }
    public List<string> autoPay { get; set; }
    public List<string> location { get; set; }
    public List<string> country { get; set; }
    public List<ShippingInfo> shippingInfo { get; set; }
    public List<SellingStatu> sellingStatus { get; set; }
    public List<ListingInfo> listingInfo { get; set; }
    public List<Condition> condition { get; set; }
    public List<string> isMultiVariationListing { get; set; }
    public List<string> topRatedListing { get; set; }
    public List<string> postalCode { get; set; }
    public List<string> galleryPlusPictureURL { get; set; }
}

public class SearchResult
{
    public string invalid_name_count { get; set; }
    public List<Item> item { get; set; }
}

public class PaginationOutput
{
    public List<string> pageNumber { get; set; }
    public List<string> entriesPerPage { get; set; }
    public List<string> totalPages { get; set; }
    public List<string> totalEntries { get; set; }
}

public class FindItemsAdvancedResponse
{
    public List<string> ack { get; set; }
    public List<string> version { get; set; }
    public List<string> timestamp { get; set; }
    public List<SearchResult> searchResult { get; set; }
    public List<PaginationOutput> paginationOutput { get; set; }
    public List<string> itemSearchURL { get; set; }
}

public class RootObject
{
    public List<FindItemsAdvancedResponse> findItemsAdvancedResponse { get; set; }
}
}