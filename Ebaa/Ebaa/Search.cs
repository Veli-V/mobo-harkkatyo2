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
        public string name_;
        public string sort_;

        public Search()
        {
            query_ = "Dark Elf";
            minPrice_ = "0";
            maxPrice_ = "15";
            resultCount_ = "20";
            store_ = "EBAY-GB";
            name_ = "Default Search";
            sort_ = "BestMatch";

        }

        public Search(
            String q,
            String mip,
            String map,
            String rc,
            String s,
            String n,
            String st)
        {
            query_ = q;
            minPrice_ = mip;
            maxPrice_ = map;
            resultCount_ = rc;
            store_ = MainPage.returnStore(s);
            name_ = n;
            sort_ = MainPage.returnSort(st);
        }


        public string createUrl()
        {
            // Luodaan kysely url
            String urlString = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsAdvanced&SERVICE-VERSION=1.0.0";
            urlString += "&GLOBAL-ID=" + store_;
            urlString += "&SECURITY-APPNAME=JanneVis-5492-4df9-8755-33362e9698f1";
            urlString += "&keywords=" + query_;
            urlString += "&paginationInput.entriesPerPage=" + resultCount_;
            urlString += "&sortOrder=" + sort_;
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

            return urlString;


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
