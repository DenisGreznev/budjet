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
    /// Логика взаимодействия для PunktPage.xaml
    /// </summary>
    public partial class PunktPage : Page
    {
        public PunktPage()
        {
            InitializeComponent();
            LoadGrid();
        }
        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Пункт";
            datagrid.Columns[2].Header = "Растояние";
            datagrid.Columns[3].Header = "Год";
            datagrid.Columns[4].Header = "Численность";
            datagrid.Columns[5].Header = "Дворы";

        }

        public void LoadGrid()
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM punkt;", sqlCon);
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
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                buttonred.IsEnabled = true;
                textboxid.Text = dr["id_punkt"].ToString();
                textboxname.Text = dr["name"].ToString();
                textboxrast.Text = dr["rast"].ToString();
                textboxgod.Text = dr["god"].ToString();
                textboxchisl.Text = dr["chisl"].ToString();
                textboxdvor.Text = dr["dvor"].ToString();
            }
            else buttonred.IsEnabled = false;
        }


        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textboxname.Text == "" || textboxrast.Text == "" || textboxgod.Text == "" || textboxchisl.Text == "" || textboxdvor.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Добавить новую запись?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SqlConnection sqlCon = LocalDB.GetDBConnection();
                        sqlCon.Open();
                        SqlCommand cmdKl = new SqlCommand("INSERT INTO punkt VALUES (@name, @rast, @god, @chisl, @dvor)", sqlCon);
                        cmdKl.CommandType = CommandType.Text;

                        cmdKl.Parameters.AddWithValue("@name", textboxname.Text);
                        cmdKl.Parameters.AddWithValue("@rast", textboxrast.Text);
                        cmdKl.Parameters.AddWithValue("@god", textboxgod.Text);
                        cmdKl.Parameters.AddWithValue("@chisl", textboxchisl.Text);
                        cmdKl.Parameters.AddWithValue("@dvor", textboxdvor.Text);

                        cmdKl.ExecuteNonQuery();

                        sqlCon.Close();
                        LoadGrid();
                        datagrid.Columns[0].Header = "ID";
                        datagrid.Columns[1].Header = "Пункт";
                        datagrid.Columns[2].Header = "Растояние";
                        datagrid.Columns[3].Header = "Год";
                        datagrid.Columns[4].Header = "Численность";
                        datagrid.Columns[5].Header = "Дворы";
                        textboxid.Clear();
                        textboxname.Text = "";
                        textboxrast.Text = "";
                        textboxgod.Text = "";
                        textboxchisl.Text = "";
                        textboxdvor.Text = "";

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonred_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_punkt"];
                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE punkt SET name='" + textboxname.Text + "', rast='" + textboxrast.Text + "', god='" + textboxgod.Text + "', chisl='" + textboxchisl.Text + "', dvor='" + textboxdvor.Text + "' WHERE id_punkt=" + id + ";", sqlCon);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Пункт";
                    datagrid.Columns[2].Header = "Растояние";
                    datagrid.Columns[3].Header = "Год";
                    datagrid.Columns[4].Header = "Численность";
                    datagrid.Columns[5].Header = "Дворы";
                    textboxid.Clear();
                    textboxname.Text = "";
                    textboxrast.Text = "";
                    textboxgod.Text = "";
                    textboxchisl.Text = "";
                    textboxdvor.Text = "";

                    datagrid.SelectedIndex = -1;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы редактировать запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Пункт";
            datagrid.Columns[2].Header = "Растояние";
            datagrid.Columns[3].Header = "Год";
            datagrid.Columns[4].Header = "Численность";
            datagrid.Columns[5].Header = "Дворы";
        }

        private void buttonclear_Click(object sender, RoutedEventArgs e)
        {

            LoadGrid();
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Пункт";
            datagrid.Columns[2].Header = "Растояние";
            datagrid.Columns[3].Header = "Год";
            datagrid.Columns[4].Header = "Численность";
            datagrid.Columns[5].Header = "Дворы";
            textboxid.Clear();
            textboxname.Text = "";
            textboxrast.Text = "";
            textboxgod.Text = "";
            textboxchisl.Text = "";
            textboxdvor.Text = "";

        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM punkt WHERE name LIKE '%" + textboxname.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Пункт";
            datagrid.Columns[2].Header = "Растояние";
            datagrid.Columns[3].Header = "Год";
            datagrid.Columns[4].Header = "Численность";
            datagrid.Columns[5].Header = "Дворы";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Пункт";
            datagrid.Columns[2].Header = "Растояние";
            datagrid.Columns[3].Header = "Год";
            datagrid.Columns[4].Header = "Численность";
            datagrid.Columns[5].Header = "Дворы";
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {


        }

       
        private void buttondel_Click_1(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_punkt"];

                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("DELETE FROM punkt WHERE id_punkt=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    datagrid.SelectedIndex = -1;
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Пункт";
                    datagrid.Columns[2].Header = "Растояние";
                    datagrid.Columns[3].Header = "Год";
                    datagrid.Columns[4].Header = "Численность";
                    datagrid.Columns[5].Header = "Дворы";
                    textboxid.Clear();
                    textboxname.Text = "";
                    textboxrast.Text = "";
                    textboxgod.Text = "";
                    textboxchisl.Text = "";
                    textboxdvor.Text = "";
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы удалить запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
