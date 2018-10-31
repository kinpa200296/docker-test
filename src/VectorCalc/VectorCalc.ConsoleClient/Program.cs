using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using VectorCalc.Client;
using VectorCalc.Infrastructure;

namespace VectorCalc.ConsoleClient
{
    class Program
    {
        public const int SumSize = 15;
        public const int MinusSize = 12;
        public const int ScalarMultiplySize = 7;

        private static Random _rand;
        private static int _testCnt;
        private static int _cnt;
        private static TaskCompletionSource<object> _taskSrc;


        static void Main(string[] args)
        {
            _rand = new Random();
            LoggerFactory.SetProvider(CreateConsoleLogger);

            _testCnt = 0;
            _cnt = 0;
            _taskSrc = new TaskCompletionSource<object>();
            var sw = Stopwatch.StartNew();

            TestBlockingClient();

            _taskSrc.Task.Wait();

            sw.Stop();
            Console.WriteLine($"Test executed in {sw.ElapsedMilliseconds} ms");
        }


        private static void TestBlockingClient()
        {
            //var client = new BlockingTcpClient(KnownValues.ServerIPLocalhost, KnownValues.PreferredPort);
            var client = new BlockingTcpClient(KnownValues.ServerIPDocker, KnownValues.PreferredPort);

            Console.WriteLine($"Testing service on {client.HostIP}:{client.Port}");

            for (var i = 0; i < 9; i++)
            {
                _testCnt++;
                ThreadPool.QueueUserWorkItem(RunSum, client);
            }

            for (var i = 0; i < 7; i++)
            {
                _testCnt++;
                ThreadPool.QueueUserWorkItem(RunMinus, client);
            }

            for (var i = 0; i < 5; i++)
            {
                _testCnt++;
                ThreadPool.QueueUserWorkItem(RunScalarMultiply, client);
            }
        }

        private static ILogger CreateConsoleLogger(string name)
        {
            return new ConsoleLogger(name);
        }

        private static double[] CreateRandomVector(int size)
        {
            var res = new double[size];
            for (var i = 0; i < size; i++)
            {
                res[i] = _rand.Next(0, 10);
            }
            return res;
        }

        private static void IncrementCnt()
        {
            _cnt++;
            if (_cnt >= _testCnt)
                _taskSrc.SetResult(null);
        }

        private static void RunSum(object state)
        {
            var client = (BlockingTcpClient)state;
            var v1 = CreateRandomVector(SumSize);
            var v2 = CreateRandomVector(SumSize);
            var res = client.Sum(v1,v2);
            Console.WriteLine($"[{string.Join(", ", v1)}] + [{string.Join(", ", v2)}] = [{string.Join(", ", res)}]");
            IncrementCnt();
        }

        private static void RunMinus(object state)
        {
            var client = (BlockingTcpClient)state;
            var v1 = CreateRandomVector(MinusSize);
            var v2 = CreateRandomVector(MinusSize);
            var res = client.Minus(v1, v2);
            Console.WriteLine($"[{string.Join(", ", v1)}] - [{string.Join(", ", v2)}] = [{string.Join(", ", res)}]");
            IncrementCnt();
        }

        private static void RunScalarMultiply(object state)
        {
            var client = (BlockingTcpClient)state;
            var v1 = CreateRandomVector(ScalarMultiplySize);
            var v2 = CreateRandomVector(ScalarMultiplySize);
            var res = client.ScalarMultiply(v1, v2);
            Console.WriteLine($"[{string.Join(", ", v1)}] * [{string.Join(", ", v2)}] = [{string.Join(", ", res)}]");
            IncrementCnt();
        }
    }
}
