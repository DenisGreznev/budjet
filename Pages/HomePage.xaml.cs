using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datepoisk.Text = "01.01.2022";
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(summa) FROM videlenie", sqlCon);
                string sum = Convert.ToString(cmd.ExecuteScalar());
                allsred.Text = sum;

                SqlCommand cmd2 = new SqlCommand("SELECT SUM(rashod) FROM osvoen WHERE status='Освоен'", sqlCon);
                string sum2 = Convert.ToString(cmd2.ExecuteScalar());
                allsred2.Text = sum2;

                SqlCommand cmd3 = new SqlCommand("SELECT SUM(rashod) FROM osvoen WHERE status='Освоен' and datan>='"+datepoisk.Text+"';", sqlCon);
                string sum3 = Convert.ToString(cmd3.ExecuteScalar());
                allsred4.Text = sum3;

                SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='Освоен' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum4 = Convert.ToString(cmd4.ExecuteScalar());
                allprog.Text = sum4;

                SqlCommand cmd5 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='В процессе' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum5 = Convert.ToString(cmd5.ExecuteScalar());
                allprog2.Text = sum5;

                SqlCommand cmd6 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='Не освоено' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum6 = Convert.ToString(cmd6.ExecuteScalar());
                allprog3.Text = sum6;


                int all1 = Convert.ToInt32(allsred.Text);
                int all2 = Convert.ToInt32(allsred2.Text);
                int all3 = all1 - all2;
               
                allsred3.Text = Convert.ToString(all3);
            



                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datepoisk_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void datepoisk_TextInput(object sender, TextCompositionEventArgs e)
        {
           
        }

        private void datepoisk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection sqlCon = LocalDB.GetDBConnection();
            try
            {
                sqlCon.Open();
                
                SqlCommand cmd3 = new SqlCommand("SELECT SUM(rashod) FROM osvoen WHERE status='Освоен' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum3 = Convert.ToString(cmd3.ExecuteScalar());
                allsred4.Text = sum3;

                SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='Освоен' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum4 = Convert.ToString(cmd4.ExecuteScalar());
                allprog.Text = sum4;

                SqlCommand cmd5 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='В процессе' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum5 = Convert.ToString(cmd5.ExecuteScalar());
                allprog2.Text = sum5;

                SqlCommand cmd6 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status='Не освоено' and datan>='" + datepoisk.Text + "';", sqlCon);
                string sum6 = Convert.ToString(cmd6.ExecuteScalar());
                allprog3.Text = sum6;






                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
