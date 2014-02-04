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

    }
}
