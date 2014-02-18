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
using System.Collections.Generic;


// Tämä tiedosto sisältää tarvittavat luokat
// Ebay hakutuloksen parsimiseen olioiksi
namespace Ebaa
{
    // Tästä alkaa JSON parserin tarvitsemat luokat
    // näihin ei ole tehty itse muuta kuinta poistettu
    // @-merkit muuttujien nimistä. Ne tuli automaattisesti
    // json2csharp.com sivun avulla, mutta aiheuttivat ongelmia
    // json syötteestä poimitaan myös @merkit pois ennen
    // parsimista
    public class PrimaryCategory
    {
        public List<string> categoryId { get; set; }
        public List<string> categoryName { get; set; }
    }

    public class SecondaryCategory
    {
        public List<string> categoryId { get; set; }
        public List<string> categoryName { get; set; }
    }

    public class ShippingServiceCost
    {
        public string __invalid_name__currencyId { get; set; }
        public string __value__ { get; set; }
    }

    public class ShippingInfo
    {
        public List<ShippingServiceCost> shippingServiceCost { get; set; }
        public List<string> shippingType { get; set; }
        public List<string> shipToLocations { get; set; }
    }

    public class CurrentPrice
    {
        public string currencyId { get; set; }
        public string __value__ { get; set; }
    }

    public class ConvertedCurrentPrice
    {
        public string __invalid_name__currencyId { get; set; }
        public string __value__ { get; set; }
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

    public class ProductId
    {
        public string __invalid_name__type { get; set; }
        public string __value__ { get; set; }
    }

    public class Item
    {
        public List<string> itemId { get; set; }
        public List<string> title { get; set; }
        public List<string> globalId { get; set; }
        public List<PrimaryCategory> primaryCategory { get; set; }
        public List<SecondaryCategory> secondaryCategory { get; set; }
        public List<string> galleryURL { get; set; }
        public List<string> viewItemURL { get; set; }
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
        public List<ProductId> productId { get; set; }
        public List<string> postalCode { get; set; }
    }

    public class SearchResult
    {
        public string __invalid_name__count { get; set; }
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
