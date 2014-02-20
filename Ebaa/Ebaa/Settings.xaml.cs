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

namespace Ebaa
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
            TextBoxDefaultSearch.Text = App.defaultSearch;
            ToggleSwitchDebug.IsChecked = App.debug;
        }

        private void save_clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            App.defaultSearch = TextBoxDefaultSearch.Text;
            App.debug = (bool)ToggleSwitchDebug.IsChecked;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}
