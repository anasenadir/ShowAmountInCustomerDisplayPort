using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.IO;
using TotalAmount.data;
using System.IO.Ports;
using System.Configuration;

namespace TotalAmount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PostgressDatabase _dbConnection;
        private static string _data_received;
        static SerialPort serialPort;
        //private WriterSerialPort _writerSerialPort;
        public MainWindow()
        {
            InitializeComponent();
            _dbConnection = PostgressDatabase.getInstance();
            _dbConnection.Initialize("localhost", "postgres", "1234", "zkt_test_db");

            //_writerSerialPort = WriterSerialPort.getInstance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string serial_number = tbSerial.Text;

            // connect to serial port
            //_writerSerialPort.write("Hello World");


            //test();

            // Check if the connection is not null
            if (_dbConnection?.GetConnection() != null)
            {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM test_table WHERE ticket_serial = @serial_number", _dbConnection.GetConnection()))
                {
                    // Use parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@serial_number", serial_number);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Access the values in each column by column name or index
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string ticket_serial = reader.GetString(reader.GetOrdinal("ticket_serial"));
                                // Get the time when the customer first entered the parking
                                DateTime subscribtion_time = reader.GetDateTime(reader.GetOrdinal("date_of_subscribe"));

                                // get the current time
                                DateTime current_time = DateTime.Now;

                                // Calculate the time the customer spends in the parking
                                TimeSpan spid_time = current_time - subscribtion_time;


                                // Get the days the customer spent in the parking
                                int pay_amount = spid_time.Days * 40;

                                // Calculate the time the customer spends in the parking in minutes
                                // int total_minutes = (spid_time.Hours * 60) + spid_time.Minutes;

                                // this value is just for testing
                                int total_minutes = 61;


                                if (total_minutes > 15 && total_minutes <= 150)
                                {

                                    //if (total_minutes <= 30)
                                    //{
                                    //    pay_amount += 2;
                                    //}
                                    //else
                                    //{
                                    //    pay_amount += (total_minutes - 1) / 30 + 2;
                                    //}
                                    pay_amount += (total_minutes - 1) / 30 + 1;
                                }

                                else if (total_minutes > 150 && total_minutes <= 300)
                                {
                                    pay_amount += (total_minutes - 1) / 30 + 3;
                                }
                                else if (total_minutes > 300 && total_minutes <= 480)
                                {
                                    pay_amount += 16;
                                }
                                else if (total_minutes > 480 && total_minutes <= 720)
                                {
                                    pay_amount += 25;
                                }
                                else if (total_minutes > 720 && total_minutes <= 920)
                                {
                                    pay_amount += 30;
                                }
                                else if (total_minutes > 920 && total_minutes <= 1400)
                                {
                                    pay_amount += 40;
                                }
                                MessageBox.Show($"{spid_time.Days} day and {total_minutes} minutes, You Have to pay {pay_amount}DH");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No row found.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Database connection is null or closed.");
            }
        }
    }
}