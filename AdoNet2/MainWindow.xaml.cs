using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace AdoNet2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        SqlDataReader reader;
        DataSet set;
        SqlDataAdapter DA;
        DataTable dt ;
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
           
            string qry = "SELECT * FROM Authors";
            DA= new SqlDataAdapter(qry, conn);
            //Fill the DataSet
            set = new DataSet();
            DA.Fill(set, "Author");
            //Update a row in DataSet Table
            DataTable dt = set.Tables["Authors"];
            dt.Rows[0]["FirstName"] = "xyz";

            string sql = "UPDATE Authors SET FirstName = Lala where Id=1";
            SqlDataAdapter adapter = new SqlDataAdapter();
            
                conn.ConnectionString = cs;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //select the update command
                adapter.UpdateCommand = cmd;
                //update the data source
                adapter.Update(set, "Authors");
                MessageBox.Show("DataBase updated !! ");
            




            

        }
        

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            using(conn=new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                set = new DataSet();
                DA = new SqlDataAdapter("select * from Authors", conn);
                DA.Fill(set, "Authors");

                MyDataGrid.ItemsSource = set.Tables[0].DefaultView;
            }
        }
    }
}
