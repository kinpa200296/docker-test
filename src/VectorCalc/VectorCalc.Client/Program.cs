using System;
using System.Net;
using System.Net.Sockets;

namespace VectorCalc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpClient("192.168.99.100", 49374);

            var sw = System.Diagnostics.Stopwatch.StartNew();

            var clientSteam = client.GetStream();

            var buffer = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            clientSteam.Write(buffer, 0, buffer.Length);

            Console.WriteLine("Waiting for reply...");

            clientSteam.Read(buffer, 0, buffer.Length);

            sw.Stop();

            client.Close();

            Console.WriteLine($"Time passed: {sw.ElapsedMilliseconds}");

            Console.ReadLine();
        }
    }
}
