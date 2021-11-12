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


#region SQL_StoredProcedures
//        CREATE PROCEDURE sp_UpdateAuthors
//@Firstname NVARCHAR(15),
//@AuthorId INT
//AS
//BEGIN
//UPDATE Authors
//SET FirstName = @Firstname  WHERE Id = @AuthorId
//END



//----------------------

//CREATE PROCEDURE sp_InsertAuthor
//@Firstname NVARCHAR(15),
//@Lastname NVARCHAR(25),
//@Id INT
//AS
//BEGIN
//INSERT INTO Authors([Id], [FirstName], [LastName])
//VALUES(@Id, @Firstname, @Lastname)
//END


//CREATE PROCEDURE sp_DeleteAuthor
//@Id INT
//AS
//BEGIN
//DELETE FROM Authors WHERE Id=@Id
//END
#endregion



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
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("sp_UpdateAuthors", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var param1 = new SqlParameter();
                param1.SqlDbType = SqlDbType.NVarChar;
                param1.ParameterName = "@Firstname";
                param1.Value = FirstnameTxtBox.Text;
                sqlCommand.Parameters.Add(param1);

                
                var param2 = new SqlParameter();
                param2.SqlDbType = SqlDbType.Int;
                param2.ParameterName = "@AuthorId";
                param2.Value = Int32.Parse(IdTxtBox.Text);
                sqlCommand.Parameters.Add(param2);

                sqlCommand.ExecuteNonQuery();


                MessageBox.Show("Firstname Updated...");
            }
            
        }
        

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("sp_InsertAuthor", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var param1 = new SqlParameter();
                param1.SqlDbType = SqlDbType.Int;
                param1.ParameterName = "@Id";
                param1.Value = Int32.Parse(IdTxtBox.Text);
                sqlCommand.Parameters.Add(param1);


                var param2 = new SqlParameter();
                param2.SqlDbType = SqlDbType.NVarChar;
                param2.ParameterName = "@Firstname";
                param2.Value = FirstnameTxtBox.Text;
                sqlCommand.Parameters.Add(param2);

                var param3 = new SqlParameter();
                param3.SqlDbType = SqlDbType.NVarChar;
                param3.ParameterName = "@Lastname";
                param3.Value = LastNameTxtBox.Text;
                sqlCommand.Parameters.Add(param3);

               var result = sqlCommand.ExecuteNonQuery();

                if (result != 0)
                {
                    MessageBox.Show("Author Inserted...");
                }
                else
                {
                    MessageBox.Show("Id is used. Select another Id...");
                }

                
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("sp_DeleteAuthor", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var param1 = new SqlParameter();
                param1.SqlDbType = SqlDbType.Int;
                param1.ParameterName = "@Id";
                param1.Value = Int32.Parse(IdTxtBox.Text);
                sqlCommand.Parameters.Add(param1);                               

                var result = sqlCommand.ExecuteNonQuery();

                if (result != 0)
                {
                    MessageBox.Show("Author Deleted...");
                }
                else
                {
                    MessageBox.Show("Id is not found. Select another Id...");
                }

            }
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
