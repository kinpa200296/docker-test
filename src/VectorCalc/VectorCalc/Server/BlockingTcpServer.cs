using System.Net;
using System.Net.Sockets;
using System.Threading;
using VectorCalc.Calculator;
using VectorCalc.Infrastructure;

namespace VectorCalc.Server
{
    public class BlockingTcpServer
    {
        private static readonly ILogger _logger = LoggerFactory.GetLogger(typeof(BlockingTcpServer).Name);

        private readonly IVectorCalculator _calculator;
        private TcpListener _listener;


        public string HostIP { get; }

        public int Port { get; }

        public bool IsStarted { get; private set; }


        public BlockingTcpServer(IVectorCalculator calculator, string hostIP, int port)
        {
            _calculator = calculator;
            HostIP = hostIP;
            Port = port;
        }


        public void StartServer()
        {
            if (IsStarted)
                return;

            IsStarted = true;
            _listener = new TcpListener(IPAddress.Parse(HostIP), Port);
            _listener.Start();
            _logger.Info($"Server started on {HostIP}:{Port}");
            ThreadPool.QueueUserWorkItem(RunServer, this);
        }

        public void StopServer()
        {
            IsStarted = false;
            _listener.Stop();
            _listener = null;
            _logger.Info($"Server stopped on {HostIP}:{Port}");
        }


        private static void RunServer(object state)
        {
            var server = (BlockingTcpServer)state;

            while (server.IsStarted)
            {
                var client = server._listener.AcceptTcpClient();
                var clientStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                clientStream.Read(buffer, 0, client.ReceiveBufferSize);

                var request = CalcRequest.FromBytes(buffer);
                CalcResult res;
                switch (request.Operation)
                {
                    case Operation.Sum:
                        res = CalcResult.From(server._calculator.Sum(request.Vector1, request.Vector2));
                        break;
                    case Operation.Minus:
                        res = CalcResult.From(server._calculator.Minus(request.Vector1, request.Vector2));
                        break;
                    case Operation.ScalarMultiply:
                        res = CalcResult.From(server._calculator.ScalarMultiply(request.Vector1, request.Vector2));
                        break;
                    default:
                        continue;
                }

                Thread.Sleep(250);

                var resBytes = res.GetBytes();
                clientStream.Write(resBytes, 0, resBytes.Length);

                client.Close();
            }
        }
    }
}
