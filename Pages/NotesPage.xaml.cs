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
    /// Lógica de interacción para NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public NotesPage()
        {
            InitializeComponent();
            textboxdaten.SelectedDate = DateTime.Now;
            fill_combo();
            LoadGrid();
            fill_combo2();


        }
       
        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
        }

        public void LoadGrid()
        {
            try
            {
                SqlConnection sqlCon = LocalDB.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SELECT osvoen.id_osv, osvoen.status, punkt.name, osvoen.datan, program.name, videlenie.summa, osvoen.rashod, (videlenie.summa-osvoen.rashod) AS [Осталось] FROM osvoen, videlenie, punkt, program WHERE punkt.id_punkt=osvoen.id_punkt and videlenie.id_vid=osvoen.id_vid and program.id_prog=videlenie.id_prog;", sqlCon);
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
                    textboxprog.Items.Add(name);
                }
                dr.Close();
                
                sqlCon.Close();
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void fill_combo2()
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM punkt", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string name = dr.GetString(1);
                    textboxpunkt.Items.Add(name);
                }
                dr.Close();

                sqlCon.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void labelidadd()
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_punkt FROM punkt WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textboxpunkt.Text);
                string id = Convert.ToString(cmd.ExecuteScalar());
                labelid1.Text = id;

                SqlCommand cmd2 = new SqlCommand("SELECT id_vid FROM videlenie WHERE summa = @summa", sqlCon);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Parameters.AddWithValue("@summa", textboxvid.Text);
                string id2 = Convert.ToString(cmd2.ExecuteScalar());
                labelid2.Text = id2;

                SqlCommand cmd3 = new SqlCommand("SELECT videlenie.id_prog FROM videlenie, program WHERE videlenie.id_prog=program.id_prog and program.name = @name", sqlCon);
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.Parameters.AddWithValue("@name", textboxprog.Text);
                string id3 = Convert.ToString(cmd3.ExecuteScalar());
                labelid3.Text = id3;

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
                textboxid.Text = dr["id_osv"].ToString();
                textboxstatus.Text = dr["status"].ToString();
               
                textboxpunkt.Text = dr["name"].ToString();
                textboxdaten.Text = dr["datan"].ToString();
                textboxprog.Text = dr["name1"].ToString();
                textboxvid.Text = dr["summa"].ToString();
                textboxrashod.Text = dr["rashod"].ToString();
                textboxost.Text = dr["Осталось"].ToString();
                labelidadd();

            }
            else buttonred.IsEnabled = false;
        }

        

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textboxstatus.Text == "" || textboxpunkt.Text == "" || textboxpunkt.Text == "" || textboxdaten.Text == "" || textboxprog.Text == "" || textboxvid.Text == "" || textboxrashod.Text == "" || textboxost.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Добавить новую запись?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SqlConnection sqlCon = LocalDB.GetDBConnection();
                        sqlCon.Open();
                        SqlCommand cmdKl = new SqlCommand("INSERT INTO osvoen VALUES (@status, @datan, @rashod, @id_punkt, @id_vid)", sqlCon);
                        cmdKl.CommandType = CommandType.Text;
                      
                        cmdKl.Parameters.AddWithValue("@status", textboxstatus.Text);
                        cmdKl.Parameters.AddWithValue("@datan", textboxdaten.Text);
                        cmdKl.Parameters.AddWithValue("@rashod", textboxrashod.Text);
                        cmdKl.Parameters.AddWithValue("@id_punkt", labelid1.Text);
                        cmdKl.Parameters.AddWithValue("@id_vid", labelid2.Text);
                        
                        cmdKl.ExecuteNonQuery();
                       
                        sqlCon.Close();
                        LoadGrid();
                        datagrid.Columns[0].Header = "ID";
                        datagrid.Columns[1].Header = "Статус";
                        datagrid.Columns[2].Header = "Пункт";
                        datagrid.Columns[3].Header = "Дата начала";
                        datagrid.Columns[4].Header = "Программа";
                        datagrid.Columns[5].Header = "Выделено";
                        datagrid.Columns[6].Header = "Израсходовано";
                        datagrid.Columns[7].Header = "Осталось";


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
                int id = (int)row["id_osv"];
                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE osvoen SET status='" + textboxstatus.Text + "', datan='" + textboxdaten.Text + "', rashod='" + textboxrashod.Text + "', id_punkt='" + labelid1.Text + "', id_vid='" + labelid2.Text + "' WHERE id_osv=" + id + ";", sqlCon);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxstatus.Text = "";
                    textboxdaten.Text = "";
                    textboxrashod.Text = "";
                    textboxpunkt.Text = "";
                    textboxprog.Text = "";
                    textboxvid.Text = "";
                    textboxost.Text = "";
                    textboxid.Clear();
                    datagrid.SelectedIndex = -1;
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Статус";
                    datagrid.Columns[2].Header = "Пункт";
                    datagrid.Columns[3].Header = "Дата начала";
                    datagrid.Columns[4].Header = "Программа";
                    datagrid.Columns[5].Header = "Выделено";
                    datagrid.Columns[6].Header = "Израсходовано";
                    datagrid.Columns[7].Header = "Осталось";
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
           
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
        }

        private void buttonclear_Click(object sender, RoutedEventArgs e)
        {

            textboxdaten.SelectedDate = DateTime.Now;
            textboxstatus.SelectedIndex = 0;
            textboxpunkt.Text = "";
            textboxprog.Text = "";
            textboxvid.Text = "";
            textboxost.Text = "";
            textboxrashod.Text = "0";
            textboxid.Clear();
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT osvoen.id_osv, osvoen.status, punkt.name, osvoen.datan, program.name, videlenie.summa, osvoen.rashod, (videlenie.summa-osvoen.rashod) AS [Осталось] FROM osvoen, videlenie, punkt, program WHERE punkt.id_punkt=osvoen.id_punkt and videlenie.id_vid=osvoen.id_vid and program.id_prog=videlenie.id_prog and osvoen.status LIKE '%" + textboxstatuspoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
            textboxstatuspoisk.Text = "";
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {

            labelidadd();
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT summa FROM videlenie WHERE id_prog = @id_prog", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@id_prog", labelid3.Text);
                string sum = Convert.ToString(cmd.ExecuteScalar());
                textboxvid.Text = sum;
                sqlCon.Close();
                if (textboxrashod.Text == "")
                {
                    textboxrashod.Text = "0";
                }
                if (textboxvid.Text != "" && textboxrashod.Text != "")
                {
                    int vid = Convert.ToInt32(textboxvid.Text);
                    int rashod = Convert.ToInt32(textboxrashod.Text);
                    int ostal = vid - rashod;
                    textboxost.Text = Convert.ToString(ostal);
                }
               
                
               
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            labelidadd();

        }

        private void textboxprog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void textboxrashod_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxvid.Text != "" && textboxrashod.Text != "")
            {
                int vid = Convert.ToInt32(textboxvid.Text);
                int rashod = Convert.ToInt32(textboxrashod.Text);
                int ostal = vid - rashod;
                textboxost.Text = Convert.ToString(ostal);
            }
            else textboxrashod.Text = "0";
        }

        private void buttondel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttondel_Click_1(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_osv"];

                try
                {
                    SqlConnection sqlCon = LocalDB.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("DELETE FROM osvoen WHERE id_osv=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    
                    textboxstatus.Text = "";
                    textboxdaten.Text = "";
                    textboxrashod.Text = "";
                    textboxpunkt.Text = "";
                    textboxprog.Text = "";
                    textboxvid.Text = "";
                    textboxost.Text = "";

                    datagrid.SelectedIndex = -1;
                    datagrid.Columns[0].Header = "ID";
                    datagrid.Columns[1].Header = "Статус";
                    datagrid.Columns[2].Header = "Пункт";
                    datagrid.Columns[3].Header = "Дата начала";
                    datagrid.Columns[4].Header = "Программа";
                    datagrid.Columns[5].Header = "Выделено";
                    datagrid.Columns[6].Header = "Израсходовано";
                    datagrid.Columns[7].Header = "Осталось";
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы удалить запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonpoisk2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT osvoen.id_osv, osvoen.status, punkt.name, osvoen.datan, program.name, videlenie.summa, osvoen.rashod, (videlenie.summa-osvoen.rashod) AS [Осталось] FROM osvoen, videlenie, punkt, program WHERE punkt.id_punkt=osvoen.id_punkt and videlenie.id_vid=osvoen.id_vid and program.id_prog=videlenie.id_prog and punkt.name LIKE '%" + textboxpunktpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
        }

        private void buttonsbros2_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "ID";
            datagrid.Columns[1].Header = "Статус";
            datagrid.Columns[2].Header = "Пункт";
            datagrid.Columns[3].Header = "Дата начала";
            datagrid.Columns[4].Header = "Программа";
            datagrid.Columns[5].Header = "Выделено";
            datagrid.Columns[6].Header = "Израсходовано";
            datagrid.Columns[7].Header = "Осталось";
            textboxpunktpoisk.Text = "";
        }
    }
}
