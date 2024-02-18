using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TotalAmount.data
{
    public class WriterSerialPort
    {
        private static readonly object padlock = new();
        private static WriterSerialPort? _instance = null;
        private string port;

        private static SerialPort serialPort;

        public static WriterSerialPort getInstance()
        {
            lock (padlock)
            {
                _instance ??= new WriterSerialPort();
                return _instance;
            }
        }

        private WriterSerialPort()
        {
            port = string.Empty;
            openConnection();
        }
        public void write(string message)
        {
            if (string.IsNullOrEmpty(port))
            {
                return;
            }
            //serialPort.Write($"\r{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
            //serialPort.Write(message);
        }

        private void openConnection()
        {
            string[] portNames = SerialPort.GetPortNames();
            int i = 1;

            if (portNames.Length == 0) 
            {
                MessageBox.Show("The is no Port Found");
                return;
            }

            while (i <= 9)
            {
                try
                {
                    serialPort = new SerialPort($"COM{i}", 9600, Parity.None, 8, StopBits.One);
                    serialPort.Open();
                    //serialPort.Write($"\r{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
                    serialPort.Write("Hello");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Error: {ex.Message}");
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                }
                i++;
            }
        }
    }
}
