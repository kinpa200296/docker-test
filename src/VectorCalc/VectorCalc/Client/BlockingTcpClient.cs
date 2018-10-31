using System;
using System.Diagnostics;
using System.Net.Sockets;
using VectorCalc.Calculator;
using VectorCalc.Infrastructure;

namespace VectorCalc.Client
{
    public class BlockingTcpClient
    {
        private static readonly ILogger _logger = LoggerFactory.GetLogger(typeof(BlockingTcpClient).Name);


        public string HostIP { get; }

        public int Port { get; }


        public BlockingTcpClient(string hostIP, int port)
        {
            HostIP = hostIP;
            Port = port;
        }


        public double[] Sum(double[] v1, double[] v2)
        {
            var request = CalcRequest.Sum(v1, v2);
            var res = ExecuteRequest(request);
            return res.Result;
        }

        public double[] Minus(double[] v1, double[] v2)
        {
            var request = CalcRequest.Minus(v1, v2);
            var res = ExecuteRequest(request);
            return res.Result;
        }

        public double ScalarMultiply(double[] v1, double[] v2)
        {
            var request = CalcRequest.ScalarMultiply(v1, v2);
            var res = ExecuteRequest(request);
            return res.Result[0];
        }


        private CalcResult ExecuteRequest(CalcRequest request)
        {
            CalcResult res = CalcResult.From(double.NaN);

            var sw = Stopwatch.StartNew();
            try
            {
                var client = new TcpClient(HostIP, Port);
                var clientStream = client.GetStream();
                var bytes = request.GetBytes();
                clientStream.Write(bytes, 0, bytes.Length);

                var buffer = new byte[client.ReceiveBufferSize];
                clientStream.Read(buffer, 0, client.ReceiveBufferSize);
                res = CalcResult.FromBytes(buffer);
                client.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to execute request");
            }
            sw.Stop();
            _logger.Info($"Request to {HostIP}:{Port} executed in {sw.ElapsedMilliseconds} ms");

            return res;
        }
    }
}
