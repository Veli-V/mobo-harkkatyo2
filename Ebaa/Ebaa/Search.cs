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
using System.Runtime.Serialization;

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
            pivotItem_ = pivotti;
        }

        public void DoSearch()
        {
            // Luodaan kysely url
            String urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0";
            urlString += "&GLOBAL-ID=" + store_;
            urlString += "&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1";
            urlString += "&keywords=" + query_;
            urlString += "&paginationInput.entriesPerPage=" + resultCount_;
            urlString += "&sortOrder=StartTimeNewest";
            urlString += "&itemFilter(0).name=ListingType";
            urlString += "&itemFilter(0).value=FixedPrice";
            urlString += "&itemFilter(1).name=MinPrice";
            urlString += "&itemFilter(1).value=" + minPrice_;
            urlString += "&itemFilter(2).name=MaxPrice";
            urlString += "&itemFilter(2).value=" + maxPrice_;
            urlString += "&affiliate.networkId=9";
            urlString += "&affiliate.trackingId=1234567890";
            urlString += "&affiliate.customId=456";
            urlString += "&RESPONSE-DATA-FORMAT=JSON";

            Uri url = new Uri(urlString);

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
                        (pivotItem_.Content as ListBox).Items.Add(item);
                    }
                }
            }
        }

        public override string ToString()
        {
            String tmp = "";
            tmp += query_ + "#";
            tmp += minPrice_ + "#";
            tmp += maxPrice_ + "#";
            tmp += resultCount_ + "#";
            tmp += store_;

            return tmp;
        }


    }
}
