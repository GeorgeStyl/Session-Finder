using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using NodeJSClient.Forms;

namespace NodeJSClient
{
    internal class Program
    {
        static MyTcpClient client;
        static DayInfoService dayService;

        [STAThread]
        static void Main(string[] args)
        {
            // Initialize DB info service
            dayService = new DayInfoService();

            // Initialize TCP client
            client = new MyTcpClient();
            client.ConnectAndStayOpen();

            // Show the Session form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Pass in the DayInfoService if your Session constructor supports it
            //Session session = new Session(dayService);
            //Application.Run(session);

            // Run the polymorfic Session form
            Application.Run(new UserDefaultSettings());

            // Register Ctrl + C handler
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Ctrl + C pressed. Shutting down...");
                client.Cleanup();
                Environment.Exit(0);
            };

            // Handle console close (X button)
            SetConsoleCtrlHandler(signal =>
            {
                Console.WriteLine("Console is closing. Cleaning up...");
                client.Cleanup();
                return false;
            }, true);
        }

        // Windows API to handle console close (X button)
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate handler, bool add);
        private delegate bool ConsoleEventDelegate(CtrlType sig);
        private enum CtrlType { CTRL_C_EVENT = 0, CTRL_CLOSE_EVENT = 2 }
    }
}
