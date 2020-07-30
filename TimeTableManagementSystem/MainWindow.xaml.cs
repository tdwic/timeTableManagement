using System;
using System.Collections.Generic;
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

using Microsoft.Data.Sqlite;

namespace TimeTableManagementSystem
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


        private void button_Click(object sender, RoutedEventArgs e)
        {

            var conString = new SqliteConnectionStringBuilder();
            conString.DataSource = "./TableManagementSystem.db";

            using (var connection = new SqliteConnection(conString.ConnectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = "CREATE TABLE Lecturer(name VARCHAR(50));";

                //tableCommand.ExecuteNonQuery();



                using (var transAction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO Lecturer VALUES('Yuvin');";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO Lecturer VALUES('Dewsara');";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO Lecturer VALUES('ME');";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO Lecturer VALUES('Ovindi');";
                    insertCmd.ExecuteNonQuery();

                    transAction.Commit();
                }

                var readData = connection.CreateCommand();
                readData.CommandText = "SELECT * FROM Lecturer;";

                using (var reader = readData.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader.GetString(0);
                        MessageBox.Show(result);
                    }
                }



            }


        }

    }
}
