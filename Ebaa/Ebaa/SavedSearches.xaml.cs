﻿using System;
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

namespace Ebaa
{
    // Luokka jolla luodaan näkymä tallennetuille hauille jotta
    // ne voidaan halutessa poistaa.
    public partial class SavedSearches : PhoneApplicationPage
    {
        public SavedSearches()
        {
            InitializeComponent();
            ListBoxSavedSearches.ItemTemplate = (DataTemplate)this.Resources["DataTemplate1"];
            updateSearches();

        }

        // Päivittää hakujen tiedot näkyviin
        private void updateSearches()
        {
            ListBoxSavedSearches.Items.Clear();
            int i = 0;

            // käy läpi kaikki tallennetut haut,
            // luo niistä apumuuttujan jonka tiedot laitetaaan
            // näkyviin ruudulle.
            foreach (Search s in App.savedSearches)
            {
                SearchItem si = new SearchItem();
                si.name_ = s.name_;
                si.id_ = i;
                i++;
                ListBoxSavedSearches.Items.Add(si);
            }
        }


        // Poistaa haun jota painettu.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            int position = (int)bt.Tag;


            // Varmistus dialogi halutaanko varmasti poistaa. 
            CustomMessageBox mb = new CustomMessageBox()
            {
                Caption = "Remove saved search",
                Message = "Are You sure you want to delete this search",
                LeftButtonContent = "Yes",
                RightButtonContent = "No",
            };
            mb.Dismissed += (ss, boxEventArgs) =>
            {
                switch (boxEventArgs.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        App.savedSearches.RemoveAt(position);
                        updateSearches();
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
    }

    // apu luokka jolla tiedot näyteään.
    // Nimi on suora kopio hauon nimestä,
    // id taas haun sijainti listassa, tämän avulla
    // poistaminen mahdollista.
    public class SearchItem
    {
        public string name_ { get; set; }
        public int id_ { get; set; }
    }
}
