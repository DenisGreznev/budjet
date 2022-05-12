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
                SqlCommand cmd = new SqlCommand("SELECT SUM(viduslug.summa) FROM viduslug", sqlCon);
                SqlCommand cmd22 = new SqlCommand("SELECT SUM(videlenie.summa) FROM videlenie", sqlCon);
                string sum = Convert.ToString(cmd.ExecuteScalar());
                string sum22 = Convert.ToString(cmd22.ExecuteScalar());

                int sum111 = Convert.ToInt32(sum);
                int sum222 = Convert.ToInt32(sum22);
                int allsum = sum111+ sum222;

                allsred.Text = Convert.ToString(allsum);

               
                SqlCommand cmd33 = new SqlCommand("SELECT SUM(rashod) FROM osvoen", sqlCon);
                SqlCommand cmd44 = new SqlCommand("SELECT SUM(rashod) FROM osvuslug", sqlCon);
                string sum33 = Convert.ToString(cmd33.ExecuteScalar());
                string sum44 = Convert.ToString(cmd44.ExecuteScalar());

                int sum333 = Convert.ToInt32(sum33);
                int sum444 = Convert.ToInt32(sum44);
                int allsum2 = sum333 + sum444;

                allsred2.Text = Convert.ToString(allsum2);

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

                SqlCommand cmd7 = new SqlCommand("SELECT SUM(summa) FROM viduslug;", sqlCon);
                string sum7 = Convert.ToString(cmd7.ExecuteScalar());
                auslugi.Text = sum7;

                SqlCommand cmd8 = new SqlCommand("SELECT SUM(rashod) FROM osvuslug", sqlCon);
                string sum8 = Convert.ToString(cmd8.ExecuteScalar());
                auslugi2.Text = sum8;

                SqlCommand cmd9 = new SqlCommand("SELECT COUNT(*) FROM uslugi", sqlCon);
                string sum9 = Convert.ToString(cmd9.ExecuteScalar());
                auslugi3.Text = sum9;

                SqlCommand cmd10 = new SqlCommand("SELECT COUNT(*) FROM osvuslug WHERE status = 'В процессе' ", sqlCon);
                string sum10 = Convert.ToString(cmd10.ExecuteScalar());
                auslugi4.Text = sum10;

                SqlCommand cmd11 = new SqlCommand("SELECT COUNT(*) FROM osvuslug WHERE status = 'Освоен' ", sqlCon);
                string sum11 = Convert.ToString(cmd11.ExecuteScalar());
                auslugi5.Text = sum11;

                SqlCommand cmd12 = new SqlCommand("SELECT COUNT(*) FROM osvuslug WHERE status = 'Не освоен' ", sqlCon);
                string sum12 = Convert.ToString(cmd12.ExecuteScalar());
                auslugi6.Text = sum12;


                SqlCommand cmd13 = new SqlCommand("SELECT SUM(summa) FROM videlenie;", sqlCon);
                string sum13 = Convert.ToString(cmd13.ExecuteScalar());
                aprog.Text = sum13;

                SqlCommand cmd14 = new SqlCommand("SELECT SUM(rashod) FROM osvoen", sqlCon);
                string sum14 = Convert.ToString(cmd14.ExecuteScalar());
                aprog2.Text = sum14;

                SqlCommand cmd15 = new SqlCommand("SELECT COUNT(*) FROM program", sqlCon);
                string sum15 = Convert.ToString(cmd15.ExecuteScalar());
                aprog3.Text = sum15;

                SqlCommand cmd16 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status = 'В процессе' ", sqlCon);
                string sum16 = Convert.ToString(cmd16.ExecuteScalar());
                aprog4.Text = sum16;

                SqlCommand cmd17 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status = 'Освоен' ", sqlCon);
                string sum17 = Convert.ToString(cmd17.ExecuteScalar());
                aprog5.Text = sum17;

                SqlCommand cmd18 = new SqlCommand("SELECT COUNT(*) FROM osvoen WHERE status = 'Не освоен' ", sqlCon);
                string sum18 = Convert.ToString(cmd18.ExecuteScalar());
                aprog6.Text = sum18;


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
