using System;
using VectorCalc.Calculator;
using VectorCalc.Infrastructure;
using VectorCalc.Server;

namespace VectorCalc.TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory.SetProvider(CreateConsoleLogger);

            var server = new BlockingTcpServer(new SimpleVectorCalculator(), KnownValues.ServerIP, KnownValues.PreferredPort);
            server.StartServer();
            Console.WriteLine("Enter exit to stop server");
            while (true)
            {
                var s = Console.ReadLine();
                if (s?.ToLowerInvariant() == "exit")
                    break;
            }
            server.StopServer();
        }


        private static ILogger CreateConsoleLogger(string name)
        {
            return new ConsoleLogger(name);
        }
    }
}
