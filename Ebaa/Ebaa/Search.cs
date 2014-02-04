using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Ebaa
{
    public class Search
    {
        public string query_;
        public string minPrice_;
        public string maxPrice_;
        public string resultCount_;
        public string store_;
        public PivotItem pivotItem_;

        public Search()
        {
            query_ = "Dark Elf";
            minPrice_ = "0";
            maxPrice_ = "15";
            resultCount_ = "20";
            store_ = "EBAY-GB";
            addPivot();
        }

        public Search(
            String q,
            String mip,
            String map,
            String rc,
            String s)
        {
            query_ = q;
            minPrice_ = mip;
            maxPrice_ = map;
            resultCount_ = rc;
            store_ = s;
            addPivot();
        }

        private void addPivot(){
            PivotItem pivotti = new PivotItem();
            pivotti.Header = query_;
            ListBox lbox = new ListBox();
            pivotti.Content = lbox;
            //lbox.ItemTemplate = (DataTemplate)Ebaa.MainPage.Resources["DataTemplate1"];
            pivotItem_ = pivotti;
        }

        public void DoSearch()
        {
            Uri url = new Uri("http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0&GLOBAL-ID=EBAY-GB&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1&keywords=Dark+elf&paginationInput.entriesPerPage=10&sortOrder=StartTimeNewest&itemFilter(0).name=ListingType&itemFilter(0).value=FixedPrice&itemFilter(1).name=MinPrice&itemFilter(1).value=0.00&itemFilter(2).name=MaxPrice&itemFilter(2).value=25.00&affiliate.networkId=9&affiliate.trackingId=1234567890&affiliate.customId=456&RESPONSE-DATA-FORMAT=JSON");

            WebClient webClient = new WebClient();

            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted_1);


        }

        void webClient_DownloadStringCompleted_1(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();

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
                        MessageBox.Show(item.itemId.ToString());
                        (pivotItem_.Content as ListBox).Items.Add(item);
                    }
                }
            }
        }


    }
}
