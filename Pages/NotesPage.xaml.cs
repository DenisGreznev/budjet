﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public NotesPage()
        {
            InitializeComponent();
            textboxdaten.SelectedDate = DateTime.Now;
        }
        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM punkt", sqlCon);
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlDataReader srd = cmd.ExecuteReader();
                dt.Load(srd);
                sqlCon.Close();
                datagrid.ItemsSource = dt.DefaultView;


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

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
    }
}
