using System;
using VectorCalc.Infrastructure;

namespace VectorCalc.TcpServer
{
    class ConsoleLogger : ILogger
    {
        public string Name { get; }


        public ConsoleLogger(string name)
        {
            Name = name;
        }


        public void Error(string message)
        {
            Console.WriteLine($"[Error] {Name} -> {message}");
        }

        public void Error(Exception ex, string message)
        {
            Console.WriteLine($"[Error] {Name} -> {message}");
            Console.WriteLine(ex.ToString());
        }

        public void Info(string message)
        {
            Console.WriteLine($"[Info] {Name} -> {message}");
        }

        public void Info(Exception ex, string message)
        {
            Console.WriteLine($"[Info] {Name} -> {message}");
            Console.WriteLine(ex.ToString());
        }
    }
}
