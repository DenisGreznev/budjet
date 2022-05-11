using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для OtchPage.xaml
    /// </summary>
    public partial class OtchPage : Page
    {
        public OtchPage()
        {
            InitializeComponent();
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonpod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonred_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonclear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {


        }

        private void buttonaccept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SredItem_Selected(object sender, RoutedEventArgs e)
        {
            PanelSred.Visibility = Visibility.Visible;
        }

        private void buttoncleardate_Click(object sender, RoutedEventArgs e)
        {
            textboxdate1.Text = "";
            textboxdate2.Text = "";
        }
    }
}
