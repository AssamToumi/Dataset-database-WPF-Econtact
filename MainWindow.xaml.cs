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
using System.Configuration;



namespace Econtact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into Database
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");
                Clear();
            }
            else
            {
                MessageBox.Show("failed to Add New Contact. Try again!");
            }

            DataTable dt = c.Select();
            dataGrid.ItemsSource = dt.DefaultView;
        }
        
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(myconnstrng);
            try
            {
                sqlconn.Open();
                string Query = "select FirstName, LastName, ContactNo, Address, Gender from tbl_contact";
                SqlCommand createCommand = new SqlCommand(Query, sqlconn);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("tbl_contact");
                dataAdp.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(myconnstrng);
            try
            {
                sqlconn.Open();
                string Query = "select FirstName, LastName, ContactNo, Address, Gender from tbl_contact";
                SqlCommand createCommand = new SqlCommand(Query, sqlconn);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("tbl_contact");
                dataAdp.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Econtact_Load(object sender, RoutedEventArgs e)
        {
            DataTable dt = c.Select();
            dataGrid.ItemsSource = dt.DefaultView;
        }

        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNumber.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxAddress.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact has been successfully Updated.");
            }
            else
            {
                MessageBox.Show("failed to Update Contact.Try Again");
            }
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null)
                
            return;

        }
    }
}