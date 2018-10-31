using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace VectorCalc.Server
{
    class Program
    {
        const int Port = 0xC0DE;

        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            Console.WriteLine($"Server started on port {Port}");
            while (true)
            {
                var client = listener.AcceptTcpClient();

                Console.WriteLine("Client accepted...");

                var clientStream = client.GetStream();

                byte[] buffer = new byte[client.ReceiveBufferSize];
                var bytesRead = clientStream.Read(buffer, 0, client.ReceiveBufferSize);

                Console.WriteLine($"Recived: [{string.Join(",", buffer.Take(bytesRead))}]");

                System.Threading.Thread.Sleep(5000);

                clientStream.Write(buffer, 0, bytesRead);

                client.Close();
            }
            listener.Stop();
        }
    }
}
