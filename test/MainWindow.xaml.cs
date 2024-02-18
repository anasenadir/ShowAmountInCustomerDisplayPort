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
using System.IO.Ports;
namespace test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;

        public MainWindow()
        {
            InitializeComponent();
            //string result = "";

            //foreach (string port in SerialPort.GetPortNames())
            //{
            //    result += $"{port} ";
            //}
            //MessageBox.Show(result);
            //string[] ports = { "COM3", "COM5", "COM4", "COM7", "COM8", "COM9" };
            //string portName = ports[3]; // Replace with the actual port name where your VFD is connected
            //int baudRate = 9600; // Set the baud rate according to your VFD specifications

            //serialPort = new SerialPort(portName, baudRate);

            //try
            //{
            //    serialPort.Open();

            //    if (serialPort.IsOpen)
            //    {
            //        // Example: Send a command to clear the display
            //        string clearCommand = "\x0C"; // ASCII control code for clear display
            //        SendData(clearCommand);

            //        // Example: Send a message to display
            //        string message = "Hello, VFD!";
            //        SendData(message);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed to open the serial port.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error: {ex.Message}");
            //}
            //finally
            //{
            //    if (serialPort.IsOpen)
            //        serialPort.Close();
            //}
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 3, 5, 6, 4, 7, 8, 11

            string[] Ports = SerialPort.GetPortNames();
            string result = "";
            foreach (string Port in Ports)
            {
                result += $"{Port} ";
            }
            try
            {
                SerialPort sp = new()
                {
                    PortName = "COM11",
                    BaudRate = 9600,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Parity = Parity.None,
                };
                sp.ReadTimeout = 100000; // 5 seconds
                sp.WriteTimeout = 100000; // 5 seconds
                sp.Open();
                sp.Write(DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SendData(string data)
        {
            try
            {
                serialPort.Write(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}");
            }
        }

    }
}