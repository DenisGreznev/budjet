using System;
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
    /// Lógica de interacción para PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage()
        {
            InitializeComponent();
            LoadGrid();
        }
        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начала";
            datagrid.Columns[3].Header = "Дата завершения";
           
        }

        public void LoadGrid()
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM program;", sqlCon);
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
                textboxid.Text = dr["id_prog"].ToString();
                textboxname.Text = dr["name"].ToString();
                textboxdaten.Text = dr["datan"].ToString();
                textboxdatek.Text = dr["datak"].ToString();
            }
            else buttonred.IsEnabled = false;
        }


        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textboxname.Text == "" || textboxdaten.Text == "" || textboxdatek.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Добавить новую запись?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SqlConnection sqlCon = LocalDB.GetDBConnection();
                        sqlCon.Open();
                        SqlCommand cmdKl = new SqlCommand("INSERT INTO program VALUES (@name, @datan, @datak)", sqlCon);
                        cmdKl.CommandType = CommandType.Text;

                        cmdKl.Parameters.AddWithValue("@name", textboxname.Text);
                        cmdKl.Parameters.AddWithValue("@datan", textboxdaten.Text);
                        cmdKl.Parameters.AddWithValue("@datak", textboxdatek.Text);
     
                        cmdKl.ExecuteNonQuery();

                        sqlCon.Close();
                        LoadGrid();
                        datagrid.Columns[0].Header = "ID";
                        datagrid.Columns[1].Header = "Программа";
                        datagrid.Columns[2].Header = "Дата начала";
                        datagrid.Columns[3].Header = "Дата завершения";
                        textboxid.Clear();
                        textboxname.Text = "";
                        textboxdaten.Text = "";
                        textboxdatek.Text = "";

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
                int id = (int)row["id_prog"];
                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE program SET name='" + textboxname.Text + "', datan='" + textboxdaten.Text + "', datak='" + textboxdatek.Text +"' WHERE id_prog=" + id + ";", sqlCon);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxid.Clear();
                    textboxname.Text = "";
                    textboxdaten.Text = "";
                    textboxdatek.Text = "";
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Программа";
                    datagrid.Columns[2].Header = "Дата начала";
                    datagrid.Columns[3].Header = "Дата завершения";

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
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начала";
            datagrid.Columns[3].Header = "Дата завершения";
        }

        private void buttonclear_Click(object sender, RoutedEventArgs e)
        {
            textboxid.Clear();
            textboxname.Text = "";
            textboxdaten.Text = "";
            textboxdatek.Text = "";
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начала";
            datagrid.Columns[3].Header = "Дата завершения";
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM program WHERE name LIKE '%" + textboxpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начала";
            datagrid.Columns[3].Header = "Дата завершения";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Программа";
            datagrid.Columns[2].Header = "Дата начала";
            datagrid.Columns[3].Header = "Дата завершения";
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {


        }

        private void buttondel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_prog"];

                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("DELETE FROM program WHERE id_prog=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxid.Clear();
                    textboxname.Text = "";
                    textboxdaten.Text = "";
                    textboxdatek.Text = "";
                    datagrid.SelectedIndex = -1;
                    textboxname.Text = "";
                    textboxdaten.Text = "";
                    textboxdatek.Text = "";
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Программа";
                    datagrid.Columns[2].Header = "Дата начала";
                    datagrid.Columns[3].Header = "Дата завершения";
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
