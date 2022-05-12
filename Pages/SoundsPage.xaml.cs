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
    /// Lógica de interacción para SoundsPage.xaml
    /// </summary>
    public partial class SoundsPage : Page
    {
        public SoundsPage()
        {
            InitializeComponent();
            fill_combo();
            LoadGrid();
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
        }

        void labelidadd()
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_prog FROM program WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", comboboxprog.Text);
                string id = Convert.ToString(cmd.ExecuteScalar());
                labelid.Text = id;
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void LoadGrid()
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT videlenie.id_vid, program.name, videlenie.datav, videlenie.summa FROM videlenie, program WHERE program.id_prog=videlenie.id_prog;", sqlCon);
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

        void fill_combo()
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM program", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(1);
                    comboboxprog.Items.Add(name);
                }
                sqlCon.Close();
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
                textboxid.Text = dr["id_vid"].ToString();
                comboboxprog.Text = dr["name"].ToString();
                textboxdate.Text = dr["datav"].ToString();
                textboxsum.Text = dr["summa"].ToString();
                labelidadd();




            }
            else buttonred.IsEnabled = false;
        }

       

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboboxprog.Text == "" || textboxdate.Text == "" || textboxsum.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Добавить новую запись?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SqlConnection sqlCon = LocalDB.GetDBConnection();
                        sqlCon.Open();
                        SqlCommand cmdKl = new SqlCommand("INSERT INTO videlenie VALUES (@id_prog, @datav, @summa)", sqlCon);
                        cmdKl.CommandType = CommandType.Text;
                        cmdKl.Parameters.AddWithValue("@id_prog", labelid.Text);
                        cmdKl.Parameters.AddWithValue("@datav", textboxdate.Text);
                        cmdKl.Parameters.AddWithValue("@summa", textboxsum.Text);
                        cmdKl.ExecuteNonQuery();
                        textboxid.Clear();
                        comboboxprog.Text = "";
                        textboxdate.Text = "";
                        textboxsum.Text = "";
                        sqlCon.Close();
                        LoadGrid();
                        
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
            try
            {
                if (datagrid.SelectedCells.Count > 0)
                {
                    DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                    int id = (int)row["id_vid"];
                    try
                    {
                        SqlConnection sqlCon = LocalDB.GetDBConnection();
                        SqlCommand cmd = new SqlCommand("UPDATE videlenie SET id_prog='" + labelid.Text + "', datav='" + textboxdate.Text + "', summa='" + textboxsum.Text + "' WHERE id_vid=" + id + ";", sqlCon);
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                        sqlCon.Close();
                        LoadGrid();
                        textboxid.Clear();
                        comboboxprog.Text = "";
                        textboxdate.Text = "";
                        textboxsum.Text = "";
                        datagrid.SelectedIndex = -1;
                        datagrid.Columns[0].Header = "ID";
                        datagrid.Columns[1].Header = "Программа";
                        datagrid.Columns[2].Header = "Дата начисления";
                        datagrid.Columns[3].Header = "Сумма";


                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else MessageBox.Show("Чтобы редактировать запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
        }

        private void buttonclear_Click(object sender, RoutedEventArgs e)
        {
            textboxid.Clear();
            comboboxprog.Text = "";
            textboxdate.Text = "";
            textboxsum.Text = "";
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
            
        }
        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT videlenie.id_vid, program.name, videlenie.datav, videlenie.summa FROM videlenie, program WHERE program.id_prog=videlenie.id_prog and program.name LIKE '%" + textboxpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
            textboxpoisk.Clear();
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            labelidadd();

        }

        private void buttonallsred_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начисления";
            datagrid.Columns[3].Header = "Сумма";
        }

        private void buttonnotall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT program.name, SUM(videlenie.summa) FROM videlenie, program WHERE program.id_prog=videlenie.id_prog GROUP BY program.name;", sqlCon);
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlDataReader srd = cmd.ExecuteReader();
                dt.Load(srd);
                sqlCon.Close();
                datagrid.ItemsSource = dt.DefaultView;
                datagrid.Columns[0].Header = "Программа"; 
                datagrid.Columns[1].Header = "Сумма";


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttondel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_vid"];

                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("DELETE FROM videlenie WHERE id_vid=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxid.Clear();
                    comboboxprog.Text = "";
                    textboxdate.Text = "";
                    textboxsum.Text = "";
                    datagrid.SelectedIndex = -1;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы удалить запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void selectuslug_Selected(object sender, RoutedEventArgs e)
        {

            ((MainWindow)System.Windows.Application.Current.MainWindow).PagesNavigation.Navigate(new System.Uri("Pages/VidUslugiPage.xaml", UriKind.RelativeOrAbsolute));

        }
    }
}
