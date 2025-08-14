using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NodeJSClient
{
    public class MyTcpClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool running = false;

        public void ConnectAndStayOpen()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 9000);
                stream = client.GetStream();
                running = true;

                Console.WriteLine("Connected. Type messages to send. Press Ctrl+C or close the window to exit.");

                // Start a thread to receive messages
                new Thread(() =>
                {
                    byte[] buffer = new byte[1024];
                    while (running)
                    {
                        try
                        {
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;

                            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Console.WriteLine("\n[Server] " + response);
                        }
                        catch { break; }
                    }
                }).Start();

                // Initial message
                string initMessage = "Hello from Client";
                byte[] initData = Encoding.UTF8.GetBytes(initMessage);
                stream.Write(initData, 0, initData.Length);

                // Input loop
                while (running)
                {
                    string input = Console.ReadLine();
                    if (input == null) break;

                    byte[] data = Encoding.UTF8.GetBytes(input);
                    stream.Write(data, 0, data.Length);
                }

                Cleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void Cleanup()
        {
            running = false;
            stream?.Close();
            client?.Close();
            Console.WriteLine("Disconnected.");
        }
    }
}
